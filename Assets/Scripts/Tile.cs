using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Tower ballista;
    [SerializeField] bool isBuildable;
    public bool IsBuildable { get { return isBuildable; } } // Property for isBuildable boolean
    
    GridManager gridManager;
    Pathfinder pathfinder;
    Vector2Int coordinates = new Vector2Int();

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
    }

    void Start()
    {
        if(gridManager != null) {
            // Convert the tile position in the world to coordinates that the grid can work with.
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
            if(!isBuildable) {
                gridManager.BlockNode(coordinates);
            }
        }
    }

    void OnMouseDown() {
        // If the node at the coordinates is traversable and will not block the path
        if(gridManager.GetNode(coordinates).isTraversable && !pathfinder.WillBlockPath(coordinates)) {
            // Instantiate a tower and return whether creation was successful or not
            bool isSuccessful = ballista.CreateTower(ballista, transform.position);
            if(isSuccessful) { 
                gridManager.BlockNode(coordinates);
                pathfinder.NotifyReceivers();
            }
        }
    }
}
