using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : MonoBehaviour
{
    private Coroutine Tickdamage;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<FeetHitbox>() != null)
        {
            Tickdamage = StartCoroutine(ApplyPoison(collision.gameObject.GetComponent<FeetHitbox>()));
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<FeetHitbox>() != null)
        {
                StopCoroutine(Tickdamage);
                Tickdamage = null;
        }
    }

    IEnumerator ApplyPoison(FeetHitbox feet)
    {
        while (true) {
            feet.DamagePlayer(5);
            yield return new WaitForSeconds(1f);
        }
    }
}
