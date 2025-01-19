using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoconutTree : MonoBehaviour
{
    /*
     * Spawns coconut prefab at start transform every random between 6-9 seconds
     */
    
    [SerializeField] private GameObject coconutPrefab;
    [SerializeField] private Transform spawnPoint;
    private void Start()
    {
        StartCoroutine(SpawnCoconut());
    }

    
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Dodo"))
        {
            Instantiate(coconutPrefab, spawnPoint.position, Quaternion.identity);
        }

    }

    private IEnumerator SpawnCoconut()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(6, 9));
            Instantiate(coconutPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}
