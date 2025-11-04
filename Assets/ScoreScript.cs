using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Text text = gameObject.GetComponent<Text>();
        text.text += PlayerPrefs.GetInt("score");
        PlayerStats stats = FindObjectOfType<PlayerStats>();
        DestroyObject(stats.gameObject);
    
    
    }
}
