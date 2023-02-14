using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// This script will now execute in both edit and play mode.
[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]

public class CoordinateLabels : MonoBehaviour
{
    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    [SerializeField] Color defaultColor = Color.black;
    [SerializeField] Color builtColor = Color.gray;
    Waypoint waypoint;

    void Awake() {
       label = GetComponent<TextMeshPro>();
       label.enabled = false;
       // Set waypoint component via parent
       waypoint = GetComponentInParent<Waypoint>();
       // Calls display coordinates once on awake in the game.
       DisplayCoordinates();
       
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
        SetLabelColor();
        ToggleLabels();
    }

    void SetLabelColor()
    {
        if(waypoint.IsBuildable) {
            label.color = defaultColor;
        }
        else {
            label.color = builtColor;
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

    void ToggleLabels()
    {
        if(Input.GetKeyDown(KeyCode.C)) {
            label.enabled = !label.IsActive();
        }
    }
}
