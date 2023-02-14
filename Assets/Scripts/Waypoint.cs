using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] Tower ballista;
    [SerializeField] bool isBuildable;
    public bool IsBuildable { get { return isBuildable; } } // Property for isBuildable boolean
    
    void OnMouseDown() {
        if(isBuildable) {
            // Instantiate a tower
            bool isBuilt = ballista.CreateTower(ballista, transform.position);
            isBuildable = !isBuilt;
        }
    }
}
