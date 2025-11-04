using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : MonoBehaviour
{
    public Vector2Int currPos {get; set;}
    public int direction {get; set;} //This makes it so that the chain can go in any Direction EXCEPT this one- this allows them to branch out more and provide more unique generations
    public Chain(Vector2Int startPos, int _direction){
        currPos = startPos;
        direction = _direction; 
        

    }

    public Vector2Int Move(Dictionary<int,Vector2Int> directions){

        if(currPos == Vector2Int.zero){
            //Make the first step opposite the direction you cannot go.
            return (currPos += directions[3-direction]);
        }

        int next = Random.Range(0,directions.Count);
        while(next == direction){
            next = Random.Range(0,directions.Count);
        }
        return (currPos += directions[next]);
    }
}
