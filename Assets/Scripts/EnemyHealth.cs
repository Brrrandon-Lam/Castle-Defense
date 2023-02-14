using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 5;
    int currentHealth = 0;

    void OnEnable()
    {
        currentHealth = maxHealth;
    }
    void OnParticleCollision(GameObject other) {
        currentHealth--;
        if(currentHealth <= 0) {
            gameObject.SetActive(false);
        }
    }
}
