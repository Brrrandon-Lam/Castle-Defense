using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int cost = 75;
    [SerializeField] float buildDelay = 1f;

    void Start() {
        StartCoroutine(Build());
    }

    public bool CreateTower(Tower tower, Vector3 position)
    {
        Economy economy = FindObjectOfType<Economy>();
        if(!economy) { return false; }
        // Instantiate our tower and subtract its cost.
        if(economy.CurrentBalance >= cost) {
            Instantiate(tower, position, Quaternion.identity);
            economy.Withdraw(cost);
            return true;
        }
        return false;
    }

    IEnumerator Build()
    {
        // Turn off the children and grandchildren of the object in the hierarchy
        foreach(Transform child in transform) {
            child.gameObject.SetActive(false);
            foreach(Transform grandchild in child) {
                grandchild.gameObject.SetActive(false);
            }
        }
        // Enable the children and grandchildren sequentially in the hierarchy
        foreach(Transform child in transform) {
            child.gameObject.SetActive(true);
            // Wait for the build delay time before moving onto the next child of the tower.
            yield return new WaitForSeconds(buildDelay);
            foreach(Transform grandchild in child) {
                grandchild.gameObject.SetActive(true);
            }
        }
        // Add a build delay to control how quickly they become active
    }
}
