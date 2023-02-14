using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{

    // List of waypoints to build a path
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
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
            Waypoint waypoint = child.GetComponent<Waypoint>();
            // Safeguard
            if(waypoint) {
                path.Add(waypoint);
            }
        }
    }

    void ReturnToStart()
    {
        // Place an enemy at the first waypoint
        transform.position = path[0].transform.position;
    }

    // Coroutine that calculates linear interpolations for position and rotation between any two waypoints
    IEnumerator FollowPath()
    {
        
        foreach(Waypoint waypoint in path)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = waypoint.transform.position;
            Quaternion startRotation = transform.rotation;
            
            Quaternion endRotation = Quaternion.LookRotation((endPosition - startPosition).normalized);
            
            float travelPercent = 0f;
            
            while(travelPercent < 1f) {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                if(travelPercent * 4 < 1f) {
                    transform.rotation = Quaternion.Lerp(startRotation, endRotation, travelPercent * 4);
                }
                yield return new WaitForEndOfFrame();
            }
        }
        
    }

    void CompletedPath()
    {
        enemy.StealIncome();
        gameObject.SetActive(false);
    }
}
