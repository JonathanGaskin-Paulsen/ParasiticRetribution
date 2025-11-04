using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
    
{
    public float PS;
    private Vector3 vel;
    public float damage;
    public bool explode;
    private float lifeSpan = 1;
    public Vector3 destination;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 mP = destination;
        Vector3 direction = gameObject.transform.position;
        Vector3 vector = mP - direction;
        vector.z = 0;
        vel = vector.normalized * PS;
        gameObject.GetComponent<Rigidbody2D>().velocity = vel;
    }

    // Update is called once per frame
    void Update()
    {
        lifeSpan -= Time.deltaTime;
        if(lifeSpan <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            foreach (ItemList i in PlayerStats.instance.items)
            {
                i.item.OnHit(i.stacks, gameObject);
            }
            collision.gameObject.GetComponent<Enemy>().takeDamage(damage);
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "Environment"){
            Destroy(gameObject);
        }   
    }
}
