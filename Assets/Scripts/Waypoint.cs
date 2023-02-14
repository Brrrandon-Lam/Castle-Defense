using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] GameObject ballista;
    [SerializeField] bool isBuildable;
    public bool IsBuildable { get { return isBuildable; } } // Property for isBuildable boolean
    
    void OnMouseDown() {
        if(isBuildable) {
            // Instantiate a tower object.
            Instantiate(ballista, gameObject.transform.position, Quaternion.identity);
            isBuildable = false;
        }
    }
}
