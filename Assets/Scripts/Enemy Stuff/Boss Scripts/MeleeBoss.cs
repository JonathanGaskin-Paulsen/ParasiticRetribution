using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBoss : Enemy
{

    public bool Stop;

    bool Attacking;
    
    bool slam;
    public float slamCooldown;
    float slamReset = 0;
    public GameObject SlamIndicator;

    bool charging;
    public GameObject locator;
    public float chargeDamage;
    public float chargeCooldown;
    private float chargeReset = 0;

    public override void Start()
    {
        base.Start();
        if (GetComponent<UnityEngine.AI.NavMeshAgent>() != null)
        {
            gameObject.AddComponent<UnityEngine.AI.NavMeshAgent>();
            agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            agent.updateRotation = false;
            agent.updateUpAxis = false;
        }
        agent.enabled = false;
        chargeReset = 0;
        pos = transform.position;
    }

    void FixedUpdate()
    {
        if (AI)
        {
            agent.enabled = true;
        }
        else
        {
            agent.enabled = false;
        }

        if (AI && !Stop)
        {
            agent.speed = speed;
            agent.SetDestination(player.transform.position);
            movement = GetComponent<Rigidbody2D>().velocity;
        }
        movement = transform.position - pos;

        if (movement.x < 0 && AI)
        {
            EnemyAnimator.SetBool("left", true);
            EnemyAnimator.SetBool("right", false);
            EnemyAnimator.SetBool("chargingLeft", false);
            EnemyAnimator.SetBool("chargingRight", false);
        }
        if (movement.x > 0 && AI)
        {
            EnemyAnimator.SetBool("right", true);
            EnemyAnimator.SetBool("left", false);
            EnemyAnimator.SetBool("chargingLeft", false);
            EnemyAnimator.SetBool("chargingRight", false);
        }
        if (movement.x < 0 && Stop)
        {
            EnemyAnimator.SetBool("chargingLeft", true);
        }
        if (movement.x > 0 && Stop)
        {
            EnemyAnimator.SetBool("chargingRight", true);
        }
        if (movement.x == 0 && movement.y == 0)
        {
            EnemyAnimator.SetBool("right", false);
            EnemyAnimator.SetBool("left", false);
            EnemyAnimator.SetBool("chargingLeft", false);
            EnemyAnimator.SetBool("chargingRight", false);
        }

        pos = transform.position;

    }
    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (AI)
        {
            if (!Attacking)
            {
                chargeReset += Time.deltaTime;
                slamReset += Time.deltaTime;

                if (slamReset >= slamCooldown && Vector3.Distance(transform.position, player.transform.position) < 3f)
                {
                    StartCoroutine(Slam());
                    slamReset = 0;
                    chargeReset = 0;
                    cooldown = 0;
                }

                else if (chargeReset >= chargeCooldown)
                {
                    StartCoroutine(Charge());
                    cooldown = 0;
                    chargeReset = 0;
                    chargeReset = 0;
                }
            }
        }
    }

    public override void takeDamage(float d){
    
         StartCoroutine(Hit());
            health -= d;
            if (health <= 0)
            {
                
                AudioSource.PlayClipAtPoint(deathAudio.clip, DungeonCamera.instance.gameObject.transform.position, 1.0f);
                RoomController.instance.checkAfterKill();
                LadderScript ladder = FindObjectsOfType<LadderScript>(true)[0];
                ladder.gameObject.SetActive(true);
                Destroy(gameObject);
            }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.GetComponent<PlayerStats>() != null && cooldown >= attackSpeed && !Attacking)
        {
            //Debug.Log("Hitting player");
            collision.gameObject.GetComponent<PlayerStats>().TakeDamage(damage);
            cooldown = 0;
            chargeReset = 0;
        }
        else
        {
            if (charging)
            {
                if (collision.gameObject.GetComponent<PlayerStats>() != null)
                {
                   // Debug.Log("Charge Hitting player");
                    collision.gameObject.GetComponent<PlayerStats>().TakeDamage(chargeDamage);
                    //collision.gameObject.GetComponent<PlayerMovement>().StunPlayer(2f);
                    cooldown = 0;
                    GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
                    movement = GetComponent<Rigidbody2D>().velocity;
                }
                else if(collision.gameObject.GetComponent<Bullet>() == null && collision.gameObject.GetComponent<Explosion>() == null)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
                    movement = GetComponent<Rigidbody2D>().velocity;
                }
            }
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.GetComponent<PlayerStats>() != null && cooldown >= attackSpeed && !Attacking)
        {
            chargeReset = .01f;
            cooldown = 0;
           // Debug.Log("Coninue Hitting player");
            collision.gameObject.GetComponent<PlayerStats>().TakeDamage(damage);

        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {


    }

    IEnumerator Charge()
    {
       // Debug.Log("Attempting a charge");
        AI = false;
        Stop = true;
        Attacking = true;
        charging = true;
        GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        movement = GetComponent<Rigidbody2D>().velocity;
        speed *= 4;
        GameObject projectile = GameObject.Instantiate(locator);
        projectile.transform.position = transform.position;
        yield return new WaitForSeconds(2f); //used to be 5
        GetComponent<Rigidbody2D>().velocity = (projectile.transform.position - transform.position).normalized * speed;
        movement = GetComponent<Rigidbody2D>().velocity;
        yield return new WaitForSeconds(1f);
        speed /= 4;
        AI = true;
        Stop = false;
        Attacking = false;
        charging = false;
        //Debug.Log("Dashed");
    }

    IEnumerator Slam()
    {
        Stop = false;
      //  Debug.Log("Attempting a slam");
        Attacking = true;
        slam = true;
        GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        movement = GetComponent<Rigidbody2D>().velocity;
        float tempSpeed = speed;
        speed = 0;
        GameObject Pound = GameObject.Instantiate(SlamIndicator);
        Pound.transform.position = transform.position;
        Pound.GetComponent<Slam>().Damage = chargeDamage * 1.5f;
        yield return new WaitForSeconds(1f);
        speed = tempSpeed;
        Attacking = false;
        slam = false;
        //Debug.Log("Dashed");
    }
    public override void stun(float t)
    {


    }
}


