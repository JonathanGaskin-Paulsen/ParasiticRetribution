using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RoomController : MonoBehaviour
{
    public static RoomController instance;
    string currentFloorName = "Lab";
    RoomData currentRoomData;
    Room currentRoom;
    public Queue<RoomData> roomQueue = new Queue<RoomData>();
    public List<Room> loadedRooms = new List<Room>();
    bool isLoadingRoom = false;
    public AudioSource openAudio;
    public AudioSource closeAudio;
    public PlayerStats Player;
    public bool locked = false;
    // Start is called before the first frame update
    void Awake(){
        if(instance == null){
            instance = this;

        }
        
    }
    public void LoadRoom(string name, int X, int Y){
        if(RoomExists(X,Y)){
            return;
        }
        RoomData newData = new RoomData();
        newData.name = name;
        newData.X = X;
        newData.Y = Y;
        //Enqueue the room 
        roomQueue.Enqueue(newData);

    }
    //This is a coroutine, so continues each time it is called
    IEnumerator LoadRoomRoutine(RoomData data){
        string roomName = currentFloorName + "_" + data.name;
        AsyncOperation LoadRoom = SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive);
        while(LoadRoom.isDone == false){
            yield return null;
        }

    }
    public void RegisterRoom(Room room){
        if(RoomExists(currentRoomData.X,currentRoomData.Y)){
            //Refuse rooms occupying same space
            Destroy(room.gameObject);
            isLoadingRoom = false;
            return;
        }
        room.transform.position = new Vector3(currentRoomData.X * room.Width,currentRoomData.Y * room.Height, 0);
    
        room.X = currentRoomData.X;
        room.Y = currentRoomData.Y;
        room.name = currentFloorName + ": " + currentRoomData.name + " (" + room.X + ", " + room.Y + ") ";
        //Lab: Room (X,Y) will be the name of rooms created
        room.transform.parent = transform; 
        isLoadingRoom = false; //Finish loading room
        if(loadedRooms.Count == 0){
            //If no rooms loaded, current room = 0,0 
            DungeonCamera.instance.currRoom = room;
        }

        //Adding it to the loaded list
        loadedRooms.Add(room);
        
    }
    public bool RoomExists(int x, int y){
        return loadedRooms.Find(room => room.X == x && room.Y == y);
    }    
    void Start()
    {
        Player = FindObjectOfType<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (locked)
        {
            checkAfterKill();
        }
        UpdateQueue();
    }

    void UpdateQueue(){
        if(isLoadingRoom){
            return;
        }
        if(roomQueue.Count == 0){
            return;
        }
        currentRoomData = roomQueue.Dequeue();
        isLoadingRoom = true;
        StartCoroutine(LoadRoomRoutine(currentRoomData));

    }
    public void onRoomEnter(Room room){
        //Update Camera
        DungeonCamera.instance.currRoom = room;
        currentRoom = room;
        //Turn on enemy AIs
        Enemy[] enemies = room.GetComponentsInChildren<Enemy>();
        foreach(Enemy e in enemies){
            e.AI = true;
        }

        foreach (ItemList i in PlayerStats.instance.items)
        {
            i.item.OnRoomEnter(i.stacks, enemies);
        }


        //Room has enemies - lock player in
        if (enemies.Length >= 1){
            closeDoors(room);
        }
        //Room doesn't have enemies in it, do nothing

    }

    public void checkAfterKill(){
        //Call this function when player defeats enemy. 
        Enemy[] enemies = currentRoom.GetComponentsInChildren<Enemy>();
        if(enemies.Length < 1){
            openDoors(currentRoom);
        }
    }
    
    public void closeDoors(Room room){
        locked = true;
        closeAudio.Play();
        Door[] doors = room.GetComponentsInChildren<Door>();
        foreach(Door door in doors){
            door.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }

    }
    public void openDoors(Room room){
        locked = false;
        openAudio.Play();
        Door[] doors = room.GetComponentsInChildren<Door>();
        foreach(Door door in doors){
            if(!door.permCollider){
                door.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
        }

    }
    public void Restart(){
        loadedRooms.Clear(); 
        roomQueue.Clear();

    }
}

public class RoomData
{
    public string name;
    public int X, Y;

}
