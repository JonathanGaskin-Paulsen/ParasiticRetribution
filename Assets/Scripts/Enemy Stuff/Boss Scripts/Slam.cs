using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slam : MonoBehaviour
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
    IEnumerator Kill()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("Slam Available");
        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, transform.localScale.x/2);
        for(int i = 0; i < collisions.Length; i++)
        {
            if(collisions[i].gameObject.GetComponent<PlayerStats>() != null)
            {
                Debug.Log("Slam Jam");
                collisions[i].gameObject.GetComponent<PlayerStats>().TakeDamage(Damage);
            }
        }
        yield return new WaitForSeconds(.25f);
        Destroy(gameObject);

    }
}
