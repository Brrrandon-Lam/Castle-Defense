using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{

    // List of waypoints to build a path
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    // Speed variable for LERP ranged from 0-1.
    [SerializeField] [Range(0, 5)] float speed = 1f;

    // Call on enable rather than start, now using an object pool.
    void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }

    void FindPath()
    {
        // Clear any existing path
        path.Clear();
        // Find our path object
        GameObject parent = GameObject.FindGameObjectWithTag("Path");
        // Iterate through the children  and add them to the path
        foreach(Transform child in parent.transform) {
            path.Add(child.GetComponent<Waypoint>());
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
        // Linear interpolation from start to end position using time
        foreach(Waypoint waypoint in path)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = waypoint.transform.position;

            Quaternion startRotation = transform.rotation;
            // Store a vector pointing in the direction we want to look
            Quaternion endRotation = Quaternion.LookRotation((endPosition - startPosition).normalized);
            // Initialize interpolation ratio (0 to 1)
            float travelPercent = 0f;
            // Increment travel percent by time, linearly interpolate from start to end position
            while(travelPercent < 1f) {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                if(travelPercent * 4 < 1f) {
                    transform.rotation = Quaternion.Lerp(startRotation, endRotation, travelPercent * 4);
                }
                yield return new WaitForEndOfFrame();
            }
        }

        gameObject.SetActive(false);
    }
}
