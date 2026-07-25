using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strike : MonoBehaviour
{
    public float Damage;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Kill());
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Coroutine to handle the strike's lifespan and damage application
    IEnumerator Kill()
    {
        yield return new WaitForSeconds(1f);
        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, transform.localScale.x / 2);
        for (int i = 0; i < collisions.Length; i++)
        {
            if (collisions[i].gameObject.GetComponent<PlayerStats>() != null)
            {
                collisions[i].gameObject.GetComponent<PlayerStats>().TakeDamage(Damage);
            }
        }
        yield return new WaitForSeconds(.26f);
        Destroy(gameObject);

    }
}
