using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{

    // List of Tiles to build a path
    [SerializeField] List<Tile> path = new List<Tile>();
    // Speed variable for LERP ranged from 0-1.
    [SerializeField] [Range(0, 5)] float speed = 1f;
    Enemy enemy;

    // Call on enable rather than start, now using an object pool.
    void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
        enemy = GetComponent<Enemy>();
    }

    void FindPath()
    {
        // Clear any existing path
        path.Clear();
        // Find our path object
        GameObject parent = GameObject.FindGameObjectWithTag("Path");
        // Iterate through the children  and add them to the path
        foreach(Transform child in parent.transform) {
            Tile tile = child.GetComponent<Tile>();
            // Safeguard
            if(tile) {
                path.Add(tile);
            }
        }
    }

    void ReturnToStart()
    {
        // Place an enemy at the first Tile
        transform.position = path[0].transform.position;
    }

    // Coroutine that calculates linear interpolations for position and rotation between any two Tiles
    IEnumerator FollowPath()
    {
        
        foreach(Tile tile in path)
        {
            bool canRotate = true;
            Vector3 startPosition = transform.position;
            Vector3 endPosition = tile.transform.position;
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
