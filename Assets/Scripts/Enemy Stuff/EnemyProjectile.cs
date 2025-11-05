using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float PS;
    public Vector3 vel;
    public float damage;
    private float lifeSpan = 5;
    public bool track;
    public PlayerMovement player;
    // Start is called before the first frame update
    void Start()
    {

        gameObject.GetComponent<Rigidbody2D>().linearVelocity = vel;
    }

    void FixedUpdate()
    {
        if (track)
        {
            GetComponent<Rigidbody2D>().linearVelocity = (player.transform.position - transform.position).normalized * PS;
        }
    }


    // Update is called once per frame
    void Update()
    {


        lifeSpan -= Time.deltaTime;
        if (lifeSpan <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.GetComponent<PlayerStats>() != null)
        {
            //Debug.Log("Hit");
            collision.gameObject.GetComponent<PlayerStats>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
