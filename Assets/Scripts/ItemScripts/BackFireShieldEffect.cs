using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BackFireShieldEffect : MonoBehaviour
{
    float damageVal;
    public float duration = 5;
    public float cooldown = 0;
    // Start is called before the first frame update
    void Start()
    {
        cooldown = 0;
        damageVal = RocketLauncher.instance.damage * .5f;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Kill());
        if (cooldown > .5)
        {
            damage();
        }
        else
        {
            cooldown += Time.deltaTime;
        }
        gameObject.transform.position = PlayerStats.instance.gameObject.transform.position;
    }


    IEnumerator Kill()
    {
        
        yield return new WaitForSeconds(duration);
        DestroyObject(gameObject);

    }

    private void damage()
    {
        Debug.Log("damage called");
        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, GetComponent<CircleCollider2D>().radius * 1.5f);
        for (int i = 0; i < collisions.Length; i++)
        {
            if (collisions[i].gameObject.GetComponent<Enemy>() != null)
            {
                collisions[i].gameObject.GetComponent<Enemy>().takeDamage(damageVal);
            }
        }
        cooldown = 0;
        
    }

}
