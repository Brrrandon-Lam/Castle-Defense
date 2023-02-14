using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] [Range(0, 50)] int objectPoolSize = 5;
    [SerializeField] [Range(0.1f, 30f)] float spawnTimer = 1f;
    GameObject[] objectPool;

    void Awake() {
        PopulatePool();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    // Throw shit into the object pool and deactivate
    void PopulatePool()
    {
        objectPool = new GameObject[objectPoolSize];
        for(int i = 0; i < objectPoolSize; i++) {
            objectPool[i] = Instantiate(enemy, transform);
            objectPool[i].SetActive(false);
        }
    }

    // Activate stuff in the pool
    void EnableObjectInPool()
    {
        for(int i = 0; i < objectPoolSize; i++) {
            if(!objectPool[i].activeInHierarchy) {
                objectPool[i].SetActive(true);
                return;
            }
        }
    }

    // Coroutine
    IEnumerator SpawnEnemies()
    {
        while(true) {
            EnableObjectInPool();
            yield return new WaitForSeconds(spawnTimer);
        }
    }
}
