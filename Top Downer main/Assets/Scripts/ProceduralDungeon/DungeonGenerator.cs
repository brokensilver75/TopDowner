using System;
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
        List<Node> roomSpaces = StructureHelper.TraverseGraphToExtractLowestLeaves(bsp.RootNode);
        RoomGenerator roomGenerator = new RoomGenerator(maxIterations, minRoomLength, minRoomWidth);
        List<RoomNode> roomList = roomGenerator.GenerateRoomsInGivenSpace(roomSpaces);

        return new List<Node>(roomList);
    }
}