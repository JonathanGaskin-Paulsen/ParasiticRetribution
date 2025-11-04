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
    public float damage;
    public float speed;
    public float attackSpeed;
    public Vector3 movement;
    public Animator EnemyAnimator;
    protected float cooldown;
    public PlayerMovement player;
    public bool AI = true;
    public bool stunned;

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
            player = GameObject.FindObjectOfType<PlayerMovement>();
        }


    }




    // Update is called once per frame
    public virtual void Update()
    {
        cooldown += Time.deltaTime;

    }

    public virtual void takeDamage(float d)
    {
        if (AI || GetComponent<SpriteRenderer>().color == Color.grey || stunned)
        {
            StartCoroutine(Hit());
            health -= d;
            if (health <= 0)
            {
                int rand = Random.Range(0, 9);
                Debug.Log(rand);
                if (rand == 8)
                {
                    GameObject explosion = GameObject.Instantiate(Resources.Load("Alienade", typeof(GameObject))) as GameObject;
                    explosion.transform.position = gameObject.transform.position;
                }
                AudioSource.PlayClipAtPoint(deathAudio.clip, DungeonCamera.instance.gameObject.transform.position, 1.0f);
                RoomController.instance.checkAfterKill();
                Destroy(gameObject);
            }
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



}
