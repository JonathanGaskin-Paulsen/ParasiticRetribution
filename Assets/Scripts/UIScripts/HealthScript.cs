using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        this.transform.GetComponent<Image>().fillAmount = PlayerStats.instance.health/PlayerStats.instance.maxHealth;


    }
}
