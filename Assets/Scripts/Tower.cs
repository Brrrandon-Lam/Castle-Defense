using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int cost = 75;
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
}
