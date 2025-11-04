using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCamera : MonoBehaviour
{
    public static DungeonCamera instance;
    public GameObject player;
    public Room currRoom;
    public bool following = false; // only used for the component rooms
    public float panningSpeed;
    // Start is called before the first frame update
    private Vector3 offset;  
    void Awake(){
        instance = this;
    }
    void Start()
    {
        if(following){
            offset = transform.position - player.transform.position;
        }
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
  
        RoomSwitch();
        if(Input.GetKeyDown(KeyCode.M)){
            if(transform.position.z == -9.7f){
                transform.position = new Vector3(transform.position.x,transform.position.y,-70.0f);
                Time.timeScale = 0f;
            }
            else{
                transform.position = new Vector3(transform.position.x,transform.position.y,-9.7f);
                Time.timeScale = 1f;
            }
            
        }
 
        
        
        
    }

    Vector3 GetCameraTarget(){
        if(currRoom == null){
            return Vector3.zero;
        }
        Vector3 roomPos = currRoom.GetCenter();
        
        roomPos.z = transform.position.z;
        return roomPos;
    }
    void FollowPlayer(){
        transform.position = player.transform.position + offset;
    }
    void RoomSwitch(){
        if(currRoom == null){
            return;
        }
        Vector3 destination = GetCameraTarget();
        transform.position = Vector3.MoveTowards(transform.position,destination,Time.deltaTime * panningSpeed);
    }

    public bool isSwitchingRoom(){
        return !(transform.position.Equals(GetCameraTarget()));
    }
  
}
