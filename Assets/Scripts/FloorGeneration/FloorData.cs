using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LabData.asset",menuName = "FloorData/Lab Data")]
public class FloorData : ScriptableObject
{
    public int iterations;

    //There are 4 number of chains. Each chain starts at (0,0), and chooses a random direction.
    //It creates a room there, and then chooses another direction, stopping at maxIterations.
}
