using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.AI;
using Random = UnityEngine.Random;
public class Enemy : MonoBehaviour
{
    public Vector3 pos;
    public float health;
    public float maxHealth;
    public float damage;
    public float speed;
    public float attackSpeed;
    public int salvageDropAmount;
    public Vector3 movement;
    public Animator EnemyAnimator;
    protected float cooldown;
    public PlayerMovement player;
    public bool AI = true;
    public bool stunned;
    public bool dead = false;
    public float poisonTimer;

    public float poisonCooldown;

    public float poisonDamage;

    public float poisonDuration = 5.0f;
    protected NavMeshAgent agent;

    public AudioSource deathAudio;

    // Start is called before the first frame update
    public virtual void Start()
    {
        if (gameObject.GetComponent<Rigidbody2D>() == null)
        {
            gameObject.AddComponent<Rigidbody2D>();
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        }
        if (gameObject.GetComponent<BoxCollider2D>() == null)
        {
            gameObject.AddComponent<BoxCollider2D>();
        }
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;



        if (player == null)
        {
            player = FindFirstObjectByType<PlayerMovement>();
        }

        scaleStats(player.GetComponents<PlayerStats>()[0].currentLevel);
    }




    // Update is called once per frame
    public virtual void Update()
    {
        if (AI)
        {
            cooldown += Time.deltaTime;
        }
        poisonCooldown += Time.deltaTime;

        if (poisonTimer > 0)
        {
            poisonTimer-= Time.deltaTime;

            if (poisonCooldown >= 1f)
            {
                takePoisonDamage(poisonDamage);
                poisonCooldown = 0;
            }
        }

    }

    public virtual void scaleStats(int level)
    {
        health += level * level * (health * 0.2f);
        maxHealth = health;
        damage = damage +  level * (damage * 0.1f);
    }

    public virtual void takeDamage(float d)
    {
        if (AI || GetComponent<SpriteRenderer>().color == Color.grey || stunned)
        {
            StartCoroutine(Hit());
            health -= d;
            if (health <= 0)
            {
                onDeath();
            }
        }
    }

    public virtual void takePoisonDamage(float d)
    {
        if(AI || GetComponent<SpriteRenderer>().color == Color.grey || stunned)
        {
            StartCoroutine(Poisoned());
            health -= d;
            if (health <= 0)
            {
                onDeath();
            }
        }
    }
    public virtual void onDeath()
    {
        if (!dead) { 
            dead = true;
            int rand = Random.Range(0, 9);
            if (rand == 8)
            {
                GameObject healthDrop = GameObject.Instantiate(Resources.Load("Alienade", typeof(GameObject))) as GameObject;
                healthDrop.transform.position = gameObject.transform.position;
            }
            PlayerStats stats = FindFirstObjectByType<PlayerStats>();
            stats.salvage += salvageDropAmount;
            AudioSource.PlayClipAtPoint(deathAudio.clip, DungeonCamera.instance.gameObject.transform.position, 1.0f);
            RoomController.instance.checkAfterKill();
            Destroy(gameObject);
        }
    }

    public virtual void Poison()
    {
        poisonTimer += 3.0f;
        if (poisonTimer > poisonDuration)
        {
            poisonTimer = poisonDuration;
        }
    }


    public virtual void stun(float t)
    {
        Debug.Log("Stunning enemy");

        if (!stunned)
        {
            Debug.Log("Stunning enemy");
            StartCoroutine(MovementCC(t));
        }
    }


    IEnumerator MovementCC(float T)
    {
        AI = false;
        stunned = true;
        GetComponent<SpriteRenderer>().color = Color.cyan;
        yield return new WaitForSeconds(T);
        GetComponent<SpriteRenderer>().color = Color.white;
        AI = true;
        stunned = false;

    }
    protected IEnumerator Hit()
    {

        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(.15f);
        if (stunned)
        {
            GetComponent<SpriteRenderer>().color = Color.cyan;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    protected IEnumerator Poisoned()
    {
        GetComponent<SpriteRenderer>().color = Color.green;
        yield return new WaitForSeconds(.15f);
        if (stunned)
        {
            GetComponent<SpriteRenderer>().color = Color.cyan;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }



}
