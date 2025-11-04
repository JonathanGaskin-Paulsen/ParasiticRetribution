using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracker : MonoBehaviour
{
    private Vector3 MPos;
    public GameObject ChaserPrefab;
    public GameObject EnemyChaser;
    // Start is called before the first frame update
    void Start()
    {
        EnemyChaser = GameObject.Instantiate(ChaserPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rotate = EnemyChaser.transform.position - transform.position;

        float r = Mathf.Atan2(rotate.y, rotate.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, r);
    }

    public void Destroytracker()
    {
        Destroy(EnemyChaser);
        Destroy(gameObject);
    }
}
