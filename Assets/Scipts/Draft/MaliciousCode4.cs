using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaliciousCode4 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject maliciousObjectPrefab;
    public float spawnInterval = 1f; // Adjust as needed
    public Transform spawnPoint; // Specify a single spawn point in the inspector

    private void Start()
    {
        // Start spawning malicious objects
        InvokeRepeating("SpawnMaliciousObject", 0f, spawnInterval);
    }

    private void SpawnMaliciousObject()
    {
        // Instantiate the malicious object at the spawn point
        Instantiate(maliciousObjectPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
