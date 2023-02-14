using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Economy : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI displayBalance;
    [SerializeField] int startingBalance = 150;
    int currentBalance = 0;
    public int CurrentBalance { get { return currentBalance; } }
    
    void Awake() {
        currentBalance = startingBalance;
        UpdateDisplay();
    }

    // Deposit money to player's pool
    public void Deposit(int amount)
    {
        currentBalance += Mathf.Abs(amount);
        UpdateDisplay();
    }

    // Withdraw money from player's pool.
    public void Withdraw(int amount)
    {
        currentBalance -= Mathf.Abs(amount);
        if(currentBalance < 0) {
            // Lose the game.
            // In our case, just relaod the scene.
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.buildIndex);
        }
        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        displayBalance.text = "Gold: " + currentBalance;
    }
}
