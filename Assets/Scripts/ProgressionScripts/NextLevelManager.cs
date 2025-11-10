using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NextLevelManager : MonoBehaviour
{
   public static NextLevelManager instance;
   void Awake(){
        DontDestroyOnLoad(gameObject);
        if (instance == null) {
            instance = this;
        }
        else {
            Object.Destroy(gameObject);
        }
   }

    public void SwitchLevels(){
        gameObject.transform.position = new Vector3(0,0,0);
        RoomController.instance.Restart();
        PlayerStats.instance.currentLevel++;
        Cursor.visible = true;
        Cursor.SetCursor(null, Vector3.zero, CursorMode.ForceSoftware);
        
        

    
        SceneManager.LoadScene("LabFloorGenerate");
   }

    

}
