using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSystem : MonoBehaviour
{
    [Header("Spawnable Prefabs")]
    public GameObject hunterPrefab;
    public GameObject rockPrefab;
    public int hunters_odds = 8;
    public int stone_odds = 5;


    [Header("Spawn Settings")]
    public Transform leftBound;
    public Transform rightBound;
    public float spawnInterval = 10f; // Time in seconds between spawns
    public int objectsPerInterval = 3;

    [Header("Stuff")] 
    private int huntersAmount = 0;
    private int rocksAmount = 0;
    public Transform dodo;
    public Transform afterTrapTransform;
    public int minimum_hunters_till_knife = 3;
    
    private List<GameObject> spawnPool = new List<GameObject>();
    public int maxHunters = 5;
    public int maxRocks = 7;

    private int ropes = 3;
    public bool canSpawnKnife = false;
    
    private int total_hunters_spawned = 0;
    void Start()
    {
        // Fill the spawn pool based on the probability ratio
        for (int i = 0; i < hunters_odds; i++) spawnPool.Add(hunterPrefab);
        for (int i = 0; i < stone_odds; i++) spawnPool.Add(rockPrefab);


        // Start the spawning coroutine
        StartCoroutine(SpawnObjects());
        
        //for debugging:
        //GetComponent<Animator>().SetTrigger("TriggerTrap");
    }

    IEnumerator SpawnObjects()
    {
        yield return new WaitForSeconds(1f);
        while (true)
        {
            
            for (int i = 0; i < objectsPerInterval; i++)
            {
                SpawnRandomObject();
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }


    void SpawnRandomObject()
    {
        // Calculate a random position between the bounds
        float randomX = Random.Range(leftBound.position.x, rightBound.position.x);
        Vector3 spawnPosition = new Vector3(randomX, leftBound.position.y, leftBound.position.z);

        // Try to spawn an object from the spawn pool
        for (int attempt = 0; attempt < 2; attempt++) // Allow two attempts
        {
            GameObject randomPrefab = spawnPool[Random.Range(0, spawnPool.Count)];

            if (CanSpawn(randomPrefab))
            {
                // Instantiate the object and increase its count
                GameObject spawnedObject = Instantiate(randomPrefab, spawnPosition, Quaternion.identity);
                increaseAmount(spawnedObject);
                spawnedObject.transform.SetParent(transform);
                return; // Successfully spawned
            }
        }

        // Fallback: Spawn a rock if all else fails
        if (rocksAmount < maxRocks)
        {
            GameObject spawnedObject = Instantiate(rockPrefab, spawnPosition, Quaternion.identity);
            spawnedObject.transform.SetParent(transform);
            increaseAmount(spawnedObject);
        }
    }

    bool CanSpawn(GameObject prefab)
    {
        // Check constraints for spawning the given prefab
        if (prefab.CompareTag("Hunter"))
        {
            if (huntersAmount >= maxHunters || rocksAmount <= huntersAmount)
            {
                return false;
            }
        }
        else if (prefab.CompareTag("Stone"))
        {
            if (rocksAmount >= maxRocks)
            {
                return false;
            }
        }
        return true;
    }

    
    private void increaseAmount(GameObject obj)
    {
        if (obj.CompareTag("Hunter"))
        {
            huntersAmount++;
            if (!canSpawnKnife)
            {
                total_hunters_spawned++;
                canSpawnKnife = total_hunters_spawned >= minimum_hunters_till_knife;
            }
            
        }
        else
        {
            rocksAmount++;
        }
    }
    
    public void decreaseAmount(GameObject obj)
    {
        if (obj.CompareTag("Hunter"))
        {
            huntersAmount--;
        }
        else
        {
            rocksAmount--;
        }
    }
    
    public Transform GetNetDodo()
    {
        return dodo.transform;
    }

    public void freeDodo()
    {
        
        // set dodo position to afterTrapTransform
        lvl1manager.instance.dodo.transform.position = afterTrapTransform.position;
        lvl1manager.instance.dodo.SetActive(true);
        Destroy(gameObject);
        lvl1manager.instance.ActivateCam(false);
    }
    public void cutRope()
    {
        ropes--;
        if (ropes <= 0)
        {
            GetComponent<Animator>().SetTrigger("FreeDodo");
        }
    }
}