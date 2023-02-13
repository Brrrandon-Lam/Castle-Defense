using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{

    // Pass in waypoints to build a path
    // Build a path then output it to the console.
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField] float CoroutineYieldTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FollowPath());
    }

    // Coroutine
    IEnumerator FollowPath()
    {
        // Prints all of the elements in the list
        foreach(Waypoint waypoint in path)
        {
            transform.position = waypoint.transform.position;
            // Every time we hit this return statement we yield control for 1 second, then continue execution.
            yield return new WaitForSeconds(CoroutineYieldTime);
        }
    }
}
