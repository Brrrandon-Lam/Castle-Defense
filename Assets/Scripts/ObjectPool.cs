using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] int objectPoolSize = 5;
    [SerializeField] float spawnTimer = 1f;
    GameObject[] objectPool;

    void Awake() {
        
        PopulatePool();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    void PopulatePool()
    {
        objectPool = new GameObject[objectPoolSize];
        for(int i = 0; i < objectPoolSize; i++) {
            objectPool[i] = Instantiate(enemy, transform);
            objectPool[i].SetActive(false);
        }
    }

    void EnableObjectInPool()
    {
        for(int i = 0; i < objectPoolSize; i++) {
            if(!objectPool[i].activeInHierarchy) {
                objectPool[i].SetActive(true);
                return;
            }
        }
    }

    IEnumerator SpawnEnemies()
    {
        for(int i = 0; i < objectPoolSize; i++) {
            // Instantiate object with ObjectPool as the parent by using transform
            EnableObjectInPool();
            yield return new WaitForSeconds(spawnTimer);
        }
    }
}
