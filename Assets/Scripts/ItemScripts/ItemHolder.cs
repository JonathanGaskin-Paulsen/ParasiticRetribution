using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Random = UnityEngine.Random;


public class ItemHolder : MonoBehaviour
{
    // Used for things that hold items.
    public GameObject[] data;
    public float bruh;

    void Start()   
    {
        StartCoroutine(WaitForRoomLoad(2f));

    }


    IEnumerator WaitForRoomLoad(float T)
    {
        yield return new WaitUntil(() => ((RoomController.instance.loadedRooms.Count != 0) && (RoomController.instance.roomQueue.Count == 0)));
        yield return new WaitForSeconds(T); //Handles race condition with the boss room since it is the last one
        int index = Random.Range(0, data.Length);
        Instantiate(data[index], transform.position, transform.rotation);                                    //Your code here

    }
}
