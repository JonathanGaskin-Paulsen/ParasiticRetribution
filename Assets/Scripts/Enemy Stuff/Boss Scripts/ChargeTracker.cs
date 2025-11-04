using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeTracker : MonoBehaviour
{
    public PlayerMovement player;
    public bool locked = false;
    public bool chaser;
    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindObjectOfType<PlayerMovement>();
        }
        if (!chaser)
            StartCoroutine(Tracker());
    }

    // Update is called once per frame
    void Update()
    {
        if (!locked)
        {
            GetComponent<Rigidbody2D>().velocity = (player.transform.position - transform.position).normalized * 8;
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        }
    }

    IEnumerator Tracker()
    {
        yield return new WaitForSeconds(2f);
        if (!locked)
        {
            locked = true;
            StartCoroutine(kill());
        }
    }
    IEnumerator kill()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

}
