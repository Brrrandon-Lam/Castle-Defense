using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Vector2Int gridSize;
    public Vector2Int GridSize { get { return gridSize; } }
    
    [Tooltip("World grid size should match the Unity Editor Snap Settings.")]
    [SerializeField] int unityGridSize = 10;
    public int UnityGridSize { get { return unityGridSize; } }
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    public Dictionary<Vector2Int, Node> Grid { get { return grid; } }
    
    void Awake()
    {
        CreateGrid();
    }

    public Node GetNode(Vector2Int key)
    {
        if(grid.ContainsKey(key)) { 
            return grid[key]; 
        }
        return null;
    }

    // If a tile has an exists in the grid, block it from being traversable
    public void BlockNode(Vector2Int coordinates)
    {
        if(grid.ContainsKey(coordinates))
        {
            grid[coordinates].isTraversable = false;
        }
    }

    // Reset all of our nodes
    public void ResetNodes()
    {
        foreach(KeyValuePair<Vector2Int, Node> entry in grid)
        {
            entry.Value.connectedTo = null;
            entry.Value.isExplored = false;
            entry.Value.isPath = false;
        }
    }

    // Convert between grid and tile coordinates, similar to DisplayCoordinates.
    // Taking a transform position and turning it into coordinates.

    // Convert world position to coordinates
    public Vector2Int GetCoordinatesFromPosition(Vector3 position)
    {
        Vector2Int coordinates = new Vector2Int();
        coordinates.x = Mathf.RoundToInt(position.x / unityGridSize);
        coordinates.y = Mathf.RoundToInt(position.z / unityGridSize);
        return coordinates;
    }

    // Convert coordinates to world position
    public Vector3 GetPositionFromCoordinates(Vector2Int coordinates) {
        Vector3 position = new Vector3();
        position.x = coordinates.x * unityGridSize;
        position.z = coordinates.y * unityGridSize;
        return position;
    }

    // Create nodes and place them into our dictionary
    void CreateGrid()
    {
        // Loop through x,y components in the grid
        for(int x = 0; x < gridSize.x; x++) {
            for(int y = 0; y < gridSize.y; y++) {
                Vector2Int coordinates = new Vector2Int(x, y);
                grid.Add(coordinates, new Node(coordinates, true));

            }
        }
    }
}
