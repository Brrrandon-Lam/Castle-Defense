using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// This script will now execute in both edit and play mode.
[ExecuteAlways]
public class CoordinateLabels : MonoBehaviour
{
    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();

    void Awake() {
       label = GetComponent<TextMeshPro>();
       // Calls display coordinates once on awake in the game.
       DisplayCoordinates();
    }
    // Initialize in 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Only calls if the application is not playing (the editor)
        if(!Application.isPlaying)
        {
            // Calculate coordinates and update the object's name.
            DisplayCoordinates();
            UpdateObjectName();
        }
    }

    void DisplayCoordinates()
    {
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);
        label.text = coordinates.x + "," + coordinates.y;
    }

    void UpdateObjectName()
    {
        // Convert our Vec2 int -> string
        transform.parent.name = coordinates.ToString();
    }
}
