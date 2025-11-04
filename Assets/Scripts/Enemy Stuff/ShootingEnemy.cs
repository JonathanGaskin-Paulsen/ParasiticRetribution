using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : Enemy
{
    public GameObject bullet;
    public float PRS;
    public bool Tracking;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
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
            if (Vector2.Distance(gameObject.transform.position, player.transform.position) > 5)
            {
                agent.SetDestination(player.transform.position);
            }
            else if (Vector2.Distance(gameObject.transform.position, player.transform.position) < 4)
            {
                Vector3 d = transform.position - player.transform.position;
                this.agent.SetDestination(d);
            }
            else
            {
                this.agent.SetDestination(transform.position);
            }
        }
    }

    public override void Update()
    {
        if (AI)
        {
            agent.enabled = true;
        }
        else
        {
            agent.enabled = false;
        }

        if (AI)
        {
            base.Update();
            if (cooldown >= attackSpeed && !stunned)
            {
                Shoot();
                cooldown = 0;
            }
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

    void Shoot()
    {
        Vector3 Temp = (player.transform.position - transform.position).normalized * PRS;
        GameObject projectile = GameObject.Instantiate(bullet);
        projectile.GetComponent<EnemyProjectile>().vel = Temp;
        projectile.GetComponent<EnemyProjectile>().damage = damage;
        projectile.GetComponent<EnemyProjectile>().track = Tracking;
        projectile.GetComponent<EnemyProjectile>().player = player;
        projectile.GetComponent<EnemyProjectile>().PS = PRS;
        projectile.transform.position = transform.position;
        Physics2D.IgnoreCollision(projectile.GetComponent<Collider2D>(), GetComponent<Collider2D>());

    }




}
