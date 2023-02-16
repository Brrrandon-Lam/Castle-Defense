using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{

    // Speed variable for LERP ranged from 0-1.
    [SerializeField] [Range(0, 5)] float speed = 1f;
    Enemy enemy;
    GridManager gridManager;
    Pathfinder pathfinder;
    // List of Nodes to build a path
    List<Node> path = new List<Node>();

    // Call on enable rather than start, now using an object pool.
    void OnEnable()
    {
        ReturnToStart();
        // Recalculate path after returning to the starting node
        RecalculatePath(true);
    }
    
    void Awake()
    {
        enemy = GetComponent<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
    }

    void RecalculatePath(bool resetPath)
    {
        // Initialize a temp variable that we assign to either the start coordinates or the current coordinates.
        Vector2Int coordinates = new Vector2Int();
        if(resetPath) {
            coordinates = pathfinder.StartCoordinates;
        }
        else {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }
        // Stop any coroutines that are running while are waiting to recalculate path
        StopAllCoroutines();
        // Clear any existing path
        path.Clear();
        // Find a new path
        path = pathfinder.GetNewPath(coordinates);
        // Start up any coroutines again
        StartCoroutine(FollowPath());
    }

    void ReturnToStart()
    {
        // Place an enemy at the first Tile
        transform.position = gridManager.GetPositionFromCoordinates(pathfinder.StartCoordinates);
    }

    // Coroutine that calculates linear interpolations for position and rotation between any two Tiles
    IEnumerator FollowPath()
    {
        // When we get a new path, do not head back to the first node in the current path. Go straight towards the next node in the path.
        for(int i = 1; i < path.Count; i++)
        {
            bool canRotate = true;
            Vector3 startPosition = transform.position;
            Vector3 endPosition = gridManager.GetPositionFromCoordinates(path[i].coordinates);
            Quaternion startRotation = transform.rotation;
            if((endPosition - startPosition) == Vector3.zero) {
                canRotate = false;   
            }
            float travelPercent = 0f;
            while(travelPercent < 1f) {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                if(canRotate && travelPercent * 4 < 1f) {
                    Quaternion endRotation = Quaternion.LookRotation((endPosition - startPosition).normalized);
                    transform.rotation = Quaternion.Lerp(startRotation, endRotation, travelPercent * 4);
                }
                yield return new WaitForEndOfFrame();
            }
        }
        CompletedPath();
    }

    void CompletedPath()
    {
        enemy.StealIncome();
        gameObject.SetActive(false);
    }
}
