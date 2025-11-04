using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FloorGenerator : MonoBehaviour
{
    // Start is called before the first frame update
   public int iterations = 3;
   public FloorGenerator instance;
   public static Dictionary<int, Vector2Int> RoomDirectionHashmap = new Dictionary<int,Vector2Int>{
        {0, Vector2Int.up},
        {1,Vector2Int.left},
        {2, Vector2Int.right},
        {3, Vector2Int.down}
    };
   private List<Vector2Int> floorRooms;
   private IEnumerator waitRoutine;

   public void Awake(){

    if(instance == null){
        instance = this;
    }

   }

   private void Start(){
        
        floorRooms = ChainController.GenerateFloor(PlayerStats.instance.currentLevel+3);
        InitRooms();

        waitRoutine = WaitForRoomLoad();
        StartCoroutine(waitRoutine);
   }
   private void InitRooms(){
    List<Vector2Int> endRooms = getEndRooms();
    if(endRooms.Count == 1){
        //Only one endroom spawned- make it so that the item room spawns next to the player.
        endRooms.Add(RoomDirectionHashmap[1]);
    }
    //No end rooms spawned- make boss and item room spawn next to player
    if(endRooms.Count == 0){
        endRooms.Add(RoomDirectionHashmap[1]);
        endRooms.Add(RoomDirectionHashmap[2]);

    }
    RoomController.instance.LoadRoom("Start",0,0);
    foreach(Vector2Int room in floorRooms){
        int randRoom = Random.Range(1, 7);
        string name = "Small_" + randRoom;
        if(room == endRooms[endRooms.Count - 1]){
            //First Endroom found - make it the boss room            
            int random = Random.Range(0,2);
            if(random == 0){
                name = "Boss";
            }
            else{
                name = "Boss_2";
            }   
        }
        else if(room == endRooms[endRooms.Count - 2]){
            //Second Endroom found - make it the item room    
            
            name = "Item";
        }
        RoomController.instance.LoadRoom(name,room.x,room.y);
    }   
    endRooms.Clear();
    floorRooms.Clear();
    }
    private List<Vector2Int> getEndRooms(){
        List<Vector2Int> returnList = new List<Vector2Int>();
        foreach(Vector2Int room in floorRooms){
            if(isEndRoom(room)){
                returnList.Add(room);
            }
        }
        return returnList;
    }
    private bool isEndRoom(Vector2Int coords){
        int neighbors = 0;
        if(roomExists(new Vector2Int(coords.x+1,coords.y))){
            neighbors++;
        }
        if(roomExists(new Vector2Int(coords.x-1,coords.y))){
            neighbors++;
        }
        if(roomExists(new Vector2Int(coords.x,coords.y+1))){
            neighbors++;
        }
        if(roomExists(new Vector2Int(coords.x,coords.y-1))){
            neighbors++;
        }
        return (neighbors == 1);
    }
    private bool roomExists(Vector2Int coords){
        foreach(Vector2Int room in floorRooms){
            if(room == coords){
                return true;
            }
        }
        return false;
    }
IEnumerator WaitForRoomLoad(){
    yield return new WaitUntil(() => ((RoomController.instance.loadedRooms.Count != 0) && (RoomController.instance.roomQueue.Count == 0)));
    yield return new WaitForSeconds(0.5f); //Handles race condition with the boss room since it is the last one
    foreach(Room room in RoomController.instance.loadedRooms){
        room.RemoveFloatingDoors();
    }
}
}