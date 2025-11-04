using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public float Width,Height;
    public int numEnemies,X,Y;
    

    public Door leftDoor,rightDoor,topDoor,bottomDoor;

    public List<Door> doors = new List<Door>();

    // Start is called before the first frame update
    void Start()
    {
        
        if(RoomController.instance == null){
            Debug.Log("RoomController not properly Initialized!");
            return;
        }
        RoomController.instance.RegisterRoom(this);
        
    

    }
    public Vector3 GetCenter(){
        return new Vector3(X*Width, Y*Height);
    }
    void OnTriggerEnter2D(Collider2D player){
        if(player.tag == "Player"){
            //Slightly pull player to center of the room so they don't get stuck on the collider.
            player.gameObject.transform.position = Vector3.MoveTowards(player.gameObject.transform.position,GetCenter(),1);
            RoomController.instance.onRoomEnter(this); 
        }

    }
    public void RemoveFloatingDoors(){
        Door[] tempDoors = GetComponentsInChildren<Door>();
        foreach(Door d in tempDoors){
            doors.Add(d);  
            switch(d.type){
                case Door.DoorType.right:
                    rightDoor = d;
                break;
                case Door.DoorType.left:
                    leftDoor = d;
                break;
                case Door.DoorType.top:
                    topDoor = d;
                break;
                case Door.DoorType.bottom:
                    bottomDoor = d;
                break;
            }
        }
        foreach(Door d in doors){
            switch(d.type){
                case Door.DoorType.right:
                    if(!RoomController.instance.RoomExists(X+1,Y)){
                        RemoveDoor(d);
                    }
                break;
                case Door.DoorType.left:
                    if(!RoomController.instance.RoomExists(X-1,Y)){
                        RemoveDoor(d);

                    }
                break;
                case Door.DoorType.top:
                    if(!RoomController.instance.RoomExists(X,Y+1)){
                        RemoveDoor(d);
                        
                    }
                break;
                case Door.DoorType.bottom:
                    if(!RoomController.instance.RoomExists(X,Y-1)){
                        RemoveDoor(d);
                    }
                break;
            }
        }
    }

    //This doesn't actually remove the door, but makes it invisible and makes the collider permanent. Any door that isn't removed is used in the open/close room logic.
    public void RemoveDoor(Door d){
        d.permCollider = true;
        d.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        d.gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }


  
}




