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
    [SerializeField] Color blockedColor = Color.gray;
    [SerializeField] Color exploredColor = Color.yellow;
    [SerializeField] Color pathColor = new Color(1f, 0f, 0.5f);
    GridManager gridManager;

    void Awake() {
       label = GetComponent<TextMeshPro>();
       label.enabled = false;
       gridManager = FindObjectOfType<GridManager>();
       


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
            label.enabled = true;
        }
        SetLabelColor();
        ToggleLabels();
    }

    void SetLabelColor()
    {
        
        if(!gridManager) {
            return;
        }
        Node node = gridManager.GetNode(coordinates);
        if(node == null) { return; }

        // Tiles that are traversable
        if(!node.isTraversable) {
            label.color = blockedColor;
        }
        else if(node.isPath) {
            label.color = pathColor;
        }
        else if(node.isExplored) {
            label.color = exploredColor;
        }
        else {
            label.color = defaultColor;
        }

        
    }

    void DisplayCoordinates()
    {
        if(gridManager == null) { return; }
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.UnityGridSize);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.UnityGridSize);
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
