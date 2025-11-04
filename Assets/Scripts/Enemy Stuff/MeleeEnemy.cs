using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
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
        pos = transform.position;
    }

    void FixedUpdate()
    {
        if (AI)
        {
            agent.speed = speed;
            agent.SetDestination(player.transform.position);
        }

        movement = transform.position - pos;
        if (movement.x < 0)
        {
            EnemyAnimator.SetBool("left", true);
            EnemyAnimator.SetBool("right", false);
        }
        if (movement.x > 0)
        {
            EnemyAnimator.SetBool("right", true);
            EnemyAnimator.SetBool("left", false);
        }
        if (movement.x == 0)
        {
            EnemyAnimator.SetBool("right", false);
            EnemyAnimator.SetBool("left", false);
        }

        pos = transform.position;
    }
    public override void Update()
    {
        
        base.Update();
        if (AI)
        {
            agent.enabled = true;
        }
        else
        {
            agent.enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.GetComponent<PlayerStats>() != null && cooldown >= attackSpeed && !stunned)
        {
            //Debug.Log("Hitting player");
            collision.gameObject.GetComponent<PlayerStats>().TakeDamage(damage);
            cooldown = 0;
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.GetComponent<PlayerStats>() != null && cooldown >= attackSpeed && !stunned)
        {
            cooldown = 0;
           // Debug.Log("Coninue Hitting player");
            collision.gameObject.GetComponent<PlayerStats>().TakeDamage(damage);
        }
    }


}
