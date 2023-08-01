using System;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class RoomGenerator
{
    private int maxIterations;
    private int minRoomLength;
    private int minRoomWidth;

    public RoomGenerator(int maxIterations, int minRoomLength, int minRoomWidth)
    {
        this.maxIterations = maxIterations;
        this.minRoomLength = minRoomLength;
        this.minRoomWidth = minRoomWidth;
    }

    public List<RoomNode> GenerateRoomsInGivenSpace(List<Node> roomSpaces, float roomBottomCornerModifier, float roomTopCornerModifier, int roomOffset)
    {
        List<RoomNode> listToReturn = new List<RoomNode>();
        foreach (var space in roomSpaces)
        {
            Vector2Int newBottomLeftPoint = StructureHelper.GenerateBottomLeftCornerBetween(
                space.BottomLeftAreaCorner,
                space.TopRightAreaCorner,
                roomBottomCornerModifier,
                roomOffset);
            Vector2Int newTopRightPoint = StructureHelper.GenerateTopRightCornerBetween(
                space.BottomLeftAreaCorner,
                space.TopRightAreaCorner,
                roomTopCornerModifier,
                roomOffset);
            space.BottomLeftAreaCorner = newBottomLeftPoint;
            space.TopLeftAreaCorner = newTopRightPoint;
            space.BottomRightAreaCorner = new Vector2Int(newTopRightPoint.x, newBottomLeftPoint.y);
            space.TopLeftAreaCorner = new Vector2Int(newBottomLeftPoint.x, newTopRightPoint.y);
            listToReturn.Add((RoomNode)space);
        }
        return listToReturn;
    }
}