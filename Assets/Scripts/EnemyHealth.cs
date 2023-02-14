using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Make Enemy a dependency of EnemyHealth
[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 5;

    [Tooltip("Adds n health to max hitpoints when an enemy dies")]
    [SerializeField] int difficultyRamp;

    int currentHealth = 0;
    Enemy enemy;

    void Start() {
        enemy = GetComponent<Enemy>();    
    }

    void OnEnable()
    {
        currentHealth = maxHealth;
    }

    void OnParticleCollision(GameObject other) {
        currentHealth--;
        if(currentHealth <= 0) {
            enemy.RewardIncome();
            maxHealth += difficultyRamp;
            gameObject.SetActive(false);
        }
    }
}
