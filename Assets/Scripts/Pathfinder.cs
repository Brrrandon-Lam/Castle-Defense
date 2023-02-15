using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    // Required nodes for our search
    [SerializeField] Vector2Int startCoordinates;
    [SerializeField] Vector2Int destinationCoordinates;
    Node startNode;
    Node destinationNode;
    Node currentSearchNode;

    // Has a node already been explored / reached?
    Dictionary<Vector2Int, Node> explored = new Dictionary<Vector2Int, Node>();
    Queue<Node> frontier = new Queue<Node>();

    // Grid and directions
    GridManager gridManager;
    Vector2Int[] directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down, Vector2Int.one, -Vector2Int.one, new Vector2Int(-1, 1), new Vector2Int(1, -1)};
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    // Call on awake
    void Awake() {
        gridManager = FindObjectOfType<GridManager>();
        if(gridManager) {
            grid = gridManager.Grid;
        }
    }

    // Call on start
    void Start() {
        // Initialize our start and end coordinates
        startNode = gridManager.Grid[startCoordinates];
        destinationNode = gridManager.Grid[destinationCoordinates];

        BFS();
        BuildPath();
    }

    void ExploreNeighbors()
    {
        // Create an empty list of neighbors
        List<Node> neighbors = new List<Node>();
        // Find all neighboring nodes
        foreach(Vector2Int direction in directions) {
            Vector2Int neighborCoordinates = currentSearchNode.coordinates + direction;
            // Check if the neighbor's coordinates exist in the grid
            if(grid.ContainsKey(neighborCoordinates)) {
                neighbors.Add(grid[neighborCoordinates]);
            }
        }

        // Keep unexplored neighbor nodes that are also traversable
        foreach(Node neighbor in neighbors)
        {
            // If we have not explored the neighbor already and we are also able to traverse the node
            if(!explored.ContainsKey(neighbor.coordinates) && neighbor.isTraversable) {
                // Create a connection on our map
                neighbor.connectedTo = currentSearchNode;
                // Add to our explored list and frontier
                explored.Add(neighbor.coordinates, neighbor);
                frontier.Enqueue(neighbor);
            }
        }
    }

    void BFS()
    {
        bool isRunning = true;
        // Add our start node to the queue and dictionary
        frontier.Enqueue(startNode);
        explored.Add(startCoordinates, startNode);

        // While the frontier is not empty and we are still running our search
        while(frontier.Count > 0 && isRunning) {
            currentSearchNode = frontier.Dequeue();
            currentSearchNode.isExplored = true;
            ExploreNeighbors();
            // If found destination
            if(currentSearchNode.coordinates == destinationCoordinates) {
                break;
            }
        }   
    }

    // Search from destination back to start
    List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node currentNode = destinationNode;

        path.Add(currentNode);
        currentNode.isPath = true;

        // Iteratively loop through all of the connected nodes.
        while(currentNode.connectedTo != null) {
            currentNode = currentNode.connectedTo;
            path.Add(currentNode);
            currentNode.isPath = true;
        }
        // Reverse list and return the path
        path.Reverse();
        return path;

    }
}
