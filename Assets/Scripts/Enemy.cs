using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int incomeReward = 25;
    [SerializeField] int incomePenalty = 25;
    // Start is called before the first frame update
    Economy economy;
    void Start()
    {
        economy = FindObjectOfType<Economy>();
    }

    // Gives income to the player.
    public void RewardIncome()
    {
        if(!economy) { return; }
        economy.Deposit(incomeReward);
    }

    // Takes income from the player
    public void StealIncome()
    {
        if(!economy) { return; }
        economy.Withdraw(incomePenalty);
    }
}
