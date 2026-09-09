using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Kill());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator Kill()
    {
        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, transform.localScale.x / 2);
        for (int i = 0; i < collisions.Length; i++)
        {
            if (collisions[i].gameObject.GetComponent<Enemy>() != null)
            {
                collisions[i].gameObject.GetComponent<Enemy>().takeDamage(damage);
            }
        }
        yield return new WaitForSeconds(.25f);
        Destroy(gameObject);

    }
}

