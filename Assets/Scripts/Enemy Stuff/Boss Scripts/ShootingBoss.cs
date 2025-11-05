using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBoss : Enemy
{
    public GameObject bullet;
    public float PRS;
    public bool Tracking;
    public GameObject Rotator;
    public bool vulnerable;

    bool ChargeAttack = false;
    float cAttackCooldown = 0;
    float cAttackSpeed;
    public GameObject Missle;

    float fAttackCooldown = 0;
    float fAttackSpeed;

    float bAttackCooldown = 0;
    float bAttackSpeed;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        cAttackSpeed = attackSpeed * 10f;
        fAttackSpeed = attackSpeed / 2f ;
        bAttackSpeed = attackSpeed * 2.1f;
        attackSpeed *= 1.35f;


    }

    void FixedUpdate()
    {
        speed = 0;
        GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, 0); ;
    }

    public override void Update()
    {
        if (AI && !ChargeAttack)
        {
            base.Update();
            fAttackCooldown += Time.deltaTime;
            cAttackCooldown += Time.deltaTime;
            bAttackCooldown += Time.deltaTime;
            if (cooldown >= 5f)
            {
                Trackingshot();
                //shoot();
                cooldown = 0;
            }
            if (fAttackCooldown >= fAttackSpeed)
            {
                FanShot();
                fAttackCooldown = 0;
            }
            if (bAttackCooldown >= bAttackSpeed)
            {
                shoot();
                bAttackCooldown = 0;
            }
            if (cAttackCooldown >= cAttackSpeed)
            {
                StartCoroutine(Strikes());
                cAttackCooldown = 0;
            }
        }
        movement = GetComponent<Rigidbody2D>().linearVelocity;
        if (movement.x < 0)
        {

        }
        if (movement.x > 0)
        {

        }
        if (movement.x == 0)
        {

        }

        Vector3 temp = (Rotator.GetComponent<Tracker>().EnemyChaser.transform.position - transform.position).normalized;


        if (temp.x < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }

    }

    public override void takeDamage(float d)
    {
        StartCoroutine(Hit());
        if (vulnerable)
        {
            health -= (d * 1.5f);
            EnemyAnimator.SetBool("idle", true);
        }
        else
        {
            health -= (d * 0.5f);
            EnemyAnimator.SetBool("idle", false);
        }



        if (health <= 0)
        {
            Rotator.GetComponent<Tracker>().Destroytracker();
            AudioSource.PlayClipAtPoint(deathAudio.clip, DungeonCamera.instance.gameObject.transform.position, 1.0f);
            RoomController.instance.checkAfterKill();
            
            LadderScript ladder = FindObjectsOfType<LadderScript>(true)[0];
            ladder.gameObject.SetActive(true);

            Destroy(gameObject);
            
        }
    }

    void shoot()
    {
        Vector3 Temp = (Rotator.GetComponent<Tracker>().EnemyChaser.transform.position - transform.position).normalized * PRS/2f;
        GameObject projectile = GameObject.Instantiate(bullet);
        projectile.transform.localScale += new Vector3(.55f, .55f, .55f);
        projectile.GetComponent<EnemyProjectile>().vel = Temp;
        projectile.GetComponent<EnemyProjectile>().damage = damage;
        projectile.GetComponent<EnemyProjectile>().track = Tracking;
        projectile.GetComponent<EnemyProjectile>().player = player;
        projectile.GetComponent<EnemyProjectile>().PS = PRS;
        projectile.transform.position = transform.position;
        Physics2D.IgnoreCollision(projectile.GetComponent<Collider2D>(), GetComponent<Collider2D>()); 
    }

    void FanShot()
    {
        Vector3 Temp1 = (Rotator.transform.right).normalized * PRS;
        GameObject projectile1 = GameObject.Instantiate(bullet);
        projectile1.GetComponent<EnemyProjectile>().vel = Temp1;
        projectile1.GetComponent<EnemyProjectile>().damage = damage;
        projectile1.GetComponent<EnemyProjectile>().track = Tracking;
        projectile1.GetComponent<EnemyProjectile>().player = player;
        projectile1.GetComponent<EnemyProjectile>().PS = PRS;
        projectile1.transform.position = transform.position;
        Physics2D.IgnoreCollision(projectile1.GetComponent<Collider2D>(), GetComponent<Collider2D>());

        Vector3 Temp2 = (Rotator.transform.right - Rotator.transform.up).normalized * PRS;
        GameObject projectile2 = GameObject.Instantiate(bullet);
        projectile2.GetComponent<EnemyProjectile>().vel = Temp2;
        projectile2.GetComponent<EnemyProjectile>().damage = damage;
        projectile2.GetComponent<EnemyProjectile>().track = Tracking;
        projectile2.GetComponent<EnemyProjectile>().player = player;
        projectile2.GetComponent<EnemyProjectile>().PS = PRS;
        projectile2.transform.position = transform.position;
        Physics2D.IgnoreCollision(projectile2.GetComponent<Collider2D>(), GetComponent<Collider2D>());

        Vector3 Temp3 = (Rotator.transform.right + Rotator.transform.up).normalized * PRS;
        GameObject projectile3 = GameObject.Instantiate(bullet);
        projectile3.GetComponent<EnemyProjectile>().vel = Temp3;
        projectile3.GetComponent<EnemyProjectile>().damage = damage;
        projectile3.GetComponent<EnemyProjectile>().track = Tracking;
        projectile3.GetComponent<EnemyProjectile>().player = player;
        projectile3.GetComponent<EnemyProjectile>().PS = PRS;
        projectile3.transform.position = transform.position;
        Physics2D.IgnoreCollision(projectile3.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    void Trackingshot()
    {
        Vector3 Temp = (Rotator.GetComponent<Tracker>().EnemyChaser.transform.position - transform.position).normalized * PRS * .75f;
        GameObject projectile = GameObject.Instantiate(bullet);
        projectile.transform.localScale += new Vector3(.25f, .25f, .25f);
        projectile.GetComponent<EnemyProjectile>().vel = Temp;
        projectile.GetComponent<EnemyProjectile>().damage = damage;
        projectile.GetComponent<EnemyProjectile>().track = Tracking;
        projectile.GetComponent<EnemyProjectile>().player = player;
        projectile.GetComponent<EnemyProjectile>().PS = PRS;
        projectile.GetComponent<EnemyProjectile>().track = true;
        projectile.transform.position = transform.position;
        Physics2D.IgnoreCollision(projectile.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    IEnumerator Strikes()
    {
        ChargeAttack = true;
        Rotator.GetComponent<Tracker>().EnemyChaser.GetComponent<ChargeTracker>().locked = true;
        for (int i = 0; i < 10; i++)
        {
            GameObject Pound = GameObject.Instantiate(Missle);
            Pound.GetComponent<Strike>().Damage = damage * 3;
            Pound.transform.position = player.transform.position;
            yield return new WaitForSeconds(.25f);
        }
        vulnerable = true;
        EnemyAnimator.SetBool("idle", true);
        yield return new WaitForSeconds(5);
        vulnerable = false;
        EnemyAnimator.SetBool("idle", false);
        ChargeAttack = false;
        Rotator.GetComponent<Tracker>().EnemyChaser.GetComponent<ChargeTracker>().locked = false;
    }

    public override void stun(float t)
    {

    }

    // Update is called once per frame


}