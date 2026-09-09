using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    bool isPaused = false;
    public bool menuOpen = false;
    public GameObject theMenu;
    public SceneTransition sceneTransition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !menuOpen && !sceneTransition.isTransitioning)
        {
            
            Pausegame();
            
        }


    }

   public void Pausegame()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
        theMenu.SetActive(!theMenu.activeSelf);
    }
    public void OnQuitButton()
    {
        Cursor.visible = true;
        PlayerStats.instance.removePlayer();
        Time.timeScale = 1f; 
        sceneTransition.moveScene("StartMenu", 0.5f);
        
    }
    public void OnRestartButton()
    {
        PlayerStats.instance.removePlayer();
        Time.timeScale = 1f;
        sceneTransition.moveScene("LabFloorGenerate", 0.5f);
    }
}
