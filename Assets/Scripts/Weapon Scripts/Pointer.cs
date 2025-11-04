using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    private Vector3 MPos;
    public GameObject Gun;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,9));
        Vector3 rotate = MPos - transform.position;

        float r = Mathf.Atan2(rotate.y, rotate.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, r);
        if((r >= 90) || (r <= -90))
        {
            Gun.GetComponent<SpriteRenderer>().flipY = true;
        }
        else
        {
            Gun.GetComponent<SpriteRenderer>().flipY = false;
        }
    }
}
