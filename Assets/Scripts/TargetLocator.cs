using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform weapon;
    Transform target;
    [SerializeField] float towerMaxRange;
    [SerializeField] ParticleSystem boltParticles;

    // Update is called once per frame
    void Update()
    {
        FindClosestTarget();
        // Rotate the turret in the direction of 
        AimWeapon();
    }

    void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closest = null;
        float minDistance = Mathf.Infinity;
        foreach(Enemy enemy in enemies) {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if(distance < minDistance) {
                closest = enemy.transform;
                minDistance = distance;
            }
        }
        target = closest;
    }

    void AimWeapon()
    {
        float targetDistance = Vector3.Distance(transform.position, target.position);
        if(target != null) {
            weapon.LookAt(target);
            if(targetDistance < towerMaxRange) {   
                // Fire
                Attack(true);
            }
            else {
                Attack(false);
            }
        }
    }
    
    void Attack(bool isActive)
    {
        var em = boltParticles.emission;
        em.enabled = isActive;
    }
}
