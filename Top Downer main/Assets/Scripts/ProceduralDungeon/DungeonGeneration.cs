using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGeneration : MonoBehaviour
{
    public int minRoomWidth, minRoomLength, dungeonLength, dungeonWidth;
    public int maxIterations;
    public int corridorWidth;
    // Start is called before the first frame update
    void Start()
    {
        CreateDungeon();
    }

    public void CreateDungeon()
    {
        DungeonGenerator generator = new DungeonGenerator(dungeonWidth, dungeonLength);
        var listOfRooms = generator.CalculateRooms(maxIterations, minRoomWidth, minRoomLength);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
