using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerStats>() != null)
        {
            collision.gameObject.GetComponent<PlayerStats>().TakePoisonDamage(5);
        }
    }

}
