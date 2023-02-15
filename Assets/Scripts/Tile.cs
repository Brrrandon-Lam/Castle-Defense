using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Tower ballista;
    [SerializeField] bool isBuildable;
    public bool IsBuildable { get { return isBuildable; } } // Property for isBuildable boolean
    
    GridManager gridManager;
    Vector2Int coordinates = new Vector2Int();

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
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
        if(isBuildable) {
            // Instantiate a tower
            bool isBuilt = ballista.CreateTower(ballista, transform.position);
            isBuildable = !isBuilt;
        }
    }
}
