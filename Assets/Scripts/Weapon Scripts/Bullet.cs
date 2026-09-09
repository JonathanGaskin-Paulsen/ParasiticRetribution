using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
    
{
    public float PS;
    private Vector3 vel;
    public float damage;
    public float ogDamage;
    public bool explode;
    private float lifeSpan = 3;
    public Vector3 destination;
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 mP = destination;
        Vector3 direction = gameObject.transform.position;
        Vector3 vector = mP - direction;
        vector.z = 0;
        vel = vector.normalized * PS;
        gameObject.GetComponent<Rigidbody2D>().linearVelocity = vel;
        ogDamage = damage;
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
    void FixedUpdate()
    {
        PlayerStats player = FindAnyObjectByType<PlayerStats>();
        foreach (ItemList i in player.items)
        {
            if (target != null)
            {
                
                i.item.OnBulletUpdate(gameObject.GetComponent<Rigidbody2D>(), target.transform.position, i.stacks);
                
            }
            i.item.OnBulletUpdate(i.stacks, gameObject.GetComponent<Bullet>());
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            foreach (ItemList i in PlayerStats.instance.items)
            {
                i.item.OnHit(i.stacks, gameObject);
                i.item.OnHit(i.stacks, collision.gameObject.GetComponent<Enemy>());
            }
            foreach (ItemList i in PlayerStats.instance.items)
            {
                i.item.onTermination(i.stacks, gameObject.GetComponent<Bullet>());
            }

            collision.gameObject.GetComponent<Enemy>().takeDamage(damage);
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "Environment"){
            Destroy(gameObject);
        }   
    }
}
