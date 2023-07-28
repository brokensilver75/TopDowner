﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class BinarySpacepartitioner
{
    RoomNode rootNode;

    public RoomNode RootNode { get => rootNode; }

    public BinarySpacepartitioner(int dungeonWidth, int dungeonLength)
    {
        this.rootNode = new RoomNode(new Vector2Int(0, 0), new Vector2Int(dungeonWidth, dungeonLength), null, 0);
    }

    public List<RoomNode> PrepareNodesCollection(int maxIterations, int minRoomWidth, int minRoomLength)
    {
        Queue<RoomNode> graph = new Queue<RoomNode>();
        List<RoomNode> listToReturn = new List<RoomNode>();
        graph.Enqueue(this.rootNode);
        listToReturn.Add(this.rootNode);
        int iterations = 0;
        while(iterations < maxIterations && graph.Count > 0) 
        {
            iterations++;
            RoomNode currentNode = graph.Dequeue();
            if(currentNode.Width >= minRoomWidth * 2 || currentNode.Length >= minRoomLength * 2)
            {
                SplitTheSpace(currentNode, listToReturn, minRoomLength, minRoomWidth, graph);
            }
        }
        return listToReturn;
    }

    public void SplitTheSpace(RoomNode currentNode, List<RoomNode> listToReturn, int minRoomLength, int minRoomWidth, Queue<RoomNode> graph)
    {
        Line line = GetLineDividingSpace(
            currentNode.BottomLeftAreaCorner,
            currentNode.TopRightAreaCorner,
            minRoomWidth,
            minRoomLength);
        RoomNode node1, node2;
        if (line.Orientation == Orientation.Horizontal)
        {
            node1 = new RoomNode(currentNode.BottomLeftAreaCorner,
                new Vector2Int(currentNode.TopRightAreaCorner.x, line.Coordinates.y),
                currentNode,
                currentNode.TreeLayerIndex + 1);

            node2  = new RoomNode(new Vector2Int(currentNode.BottomLeftAreaCorner.x, line.Coordinates.y),
                currentNode.TopRightAreaCorner,
                currentNode,
                currentNode.TreeLayerIndex + 1);
        }
        else
        {
            node1 = new RoomNode(currentNode.BottomLeftAreaCorner,
                new Vector2Int(line.Coordinates.x, currentNode.TopRightAreaCorner.y),
                currentNode,
                currentNode.TreeLayerIndex + 1);

            node2 = new RoomNode(new Vector2Int(line.Coordinates.x, currentNode.BottomLeftAreaCorner.y),
                currentNode.TopRightAreaCorner,
                currentNode,
                currentNode.TreeLayerIndex + 1);
        }
        AddNewNodeToCollections(listToReturn, graph, node1);
        AddNewNodeToCollections(listToReturn, graph, node2);
    }

    private void AddNewNodeToCollections(List<RoomNode> listToReturn, Queue<RoomNode> graph, RoomNode node)
    {
        listToReturn.Add(node);
        graph.Enqueue(node);
    }

    private Line GetLineDividingSpace(Vector2Int bottomLeftAreaCorner, Vector2Int topRightAreaCorner, int minRoomWidth, int minRoomLength)
    {
        Orientation orientation;

        bool lengthStatus = (topRightAreaCorner.y - bottomLeftAreaCorner.y) >= 2 * minRoomLength;
        bool widthStatus = (topRightAreaCorner.x - bottomLeftAreaCorner.x) >= 2 * minRoomWidth;
        if (lengthStatus && widthStatus)
        {
            orientation = (Orientation)(Random.Range(0, 2));
        }
        else if (widthStatus)
        {
            orientation = Orientation.Vertical;
        }
        else
        {
            orientation = Orientation.Horizontal;
        }

        return new Line(orientation, GetCoordinatesForOrientation(
            orientation,
            bottomLeftAreaCorner,
            topRightAreaCorner,
            minRoomWidth,
            minRoomLength));
    }

    private Vector2Int GetCoordinatesForOrientation(Orientation orientation, Vector2Int bottomLeftAreaCorner, Vector2Int topRightAreaCorner, int minRoomWidth, int minRoomLength)
    {
        Vector2Int coordinates = Vector2Int.zero;
        if (orientation == Orientation.Horizontal)
        {
            coordinates = new Vector2Int(
                0, 
                Random.Range((bottomLeftAreaCorner.y + minRoomLength),
                (topRightAreaCorner.y - minRoomLength)));
        }
        else
        {
            coordinates = new Vector2Int(
                Random.Range((bottomLeftAreaCorner.x + minRoomWidth),
                (topRightAreaCorner.x - minRoomWidth)),
                0);
        }
        return coordinates;
    }
}