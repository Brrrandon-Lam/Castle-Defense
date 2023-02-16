using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Pure C# class
[System.Serializable]
public class Node
{
    public Vector2Int coordinates;
    public bool isTraversable;
    public bool isExplored;
    public bool isPath;
    public Node connectedTo;

    public int gCost;
    public int hCost;
    public int fCost;

    // Constructor
    public Node(Vector2Int coordinates, bool isTraversable)
    {
        this.coordinates = coordinates;
        this.isTraversable = isTraversable;
    }

    // Calculate a node's f-cost
    public void CalculateFCost()
    {
        this.fCost = gCost + hCost;
    }
}
