using NavMeshPlus.Components;
using NavMeshPlus.Extensions;
using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class NavmeshGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public NavMeshSurface Surface2D;
    private IEnumerator waitRoutine;
    void Start()
    {
        StartCoroutine(WaitForRoomLoad(2f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadNavMesh(float t)
    {
        StartCoroutine(WaitForRoomLoad(t));
    }

    IEnumerator WaitForRoomLoad(float T)
    {
        yield return new WaitUntil(() => ((RoomController.instance.loadedRooms.Count != 0) && (RoomController.instance.roomQueue.Count == 0)));
        yield return new WaitForSeconds(T); //Handles race condition with the boss room since it is the last one
        Surface2D.BuildNavMeshAsync();                                       //Your code here

    }
}
