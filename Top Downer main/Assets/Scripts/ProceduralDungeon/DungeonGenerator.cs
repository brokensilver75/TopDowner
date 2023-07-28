﻿using System;
using System.Collections.Generic;
using UnityEngine;

internal class DungeonGenerator
{
    
    List<RoomNode> allSpaceNodes = new List<RoomNode>();
    private int dungeonWidth;
    private int dungeonLength;
    public DungeonGenerator(int dungeonWidth, int dungeonLength)
    {
        this.dungeonWidth = dungeonWidth;
        this.dungeonLength = dungeonLength;
    }

    public List<Node> CalculateRooms(int maxIterations, int minRoomWidth, int minRoomLength)
    {
        BinarySpacepartitioner bsp = new BinarySpacepartitioner(dungeonWidth, dungeonLength);
        allSpaceNodes = bsp.PrepareNodesCollection(maxIterations, minRoomWidth, minRoomLength);
        return new List<Node>(allSpaceNodes);
    }
}