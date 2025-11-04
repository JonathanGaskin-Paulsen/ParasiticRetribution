using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainController : MonoBehaviour
{
    public static List<Vector2Int> RoomsVisited = new List<Vector2Int>();
       
    //This just maps each symbolic direction into its vector2int equivalent. It has to be Vector2int because the room algorithm uses
    // integer x,y values to denote room positions. It has to be a map so that we can do  Dictionary(random(1-4)) and it returns a vector
    public static Dictionary<int, Vector2Int> RoomDirectionHashmap = new Dictionary<int,Vector2Int>{
        {0, Vector2Int.up},
        {1,Vector2Int.left},
        {2, Vector2Int.right},
        {3, Vector2Int.down}
    };

    public static List<Vector2Int> GenerateFloor(int iterations){
        //Return a list of positions after iterating through each chain.
        List<Chain> chains = new List<Chain>();
       for(int i = 0; i < 4; i++){
            chains.Add(new Chain(Vector2Int.zero,i));
       }
       
       for(int i = 0; i < iterations; i++){
            foreach(Chain chain in chains){
                RoomsVisited.Add(chain.Move(RoomDirectionHashmap));
            }
        }
        return RoomsVisited;

    }
}
