using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player"){
            NextLevelManager.instance.SwitchLevels();

        }
    }
}
