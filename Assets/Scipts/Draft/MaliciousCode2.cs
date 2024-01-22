using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaliciousCode2 : MonoBehaviour
{
    // The collaborative object to block.
    public Transform collaborativeObject;

    // The maximum number of malicious objects to spawn.
    public int maxObjects = 10;

    // The minimum and maximum spawn radius.
    public float minSpawnRadius = 1f;
    public float maxSpawnRadius = 5f;

    // The minimum and maximum spawn height.
    public float minSpawnHeight = 0f;
    public float maxSpawnHeight = 2f;

    // The list of spawned malicious objects.
    private List<GameObject> maliciousObjects = new List<GameObject>();

    void Start()
    {
        // Spawn a random number of malicious objects.
        for (int i = 0; i < Random.Range(1, maxObjects + 1); i++)
        {
            // Spawn a new malicious object at a random location.
            GameObject maliciousObject = new GameObject("MaliciousObject");
            maliciousObject.transform.position = Random.insideUnitSphere * maxSpawnRadius + collaborativeObject.position;

            // Create a temporary Vector3 variable and assign the value of Transform.position to it.
            Vector3 spawnPosition = maliciousObject.transform.position;

            // Modify the y component of the vector and assign it back to Transform.position.
            spawnPosition.y += Random.Range(minSpawnHeight, maxSpawnHeight);
            maliciousObject.transform.position = spawnPosition;

            // Add the malicious object to the list of spawned objects.
            maliciousObjects.Add(maliciousObject);
        }
    }

    void Update()
    {
        // Move the malicious objects towards the collaborative object.
        foreach (GameObject maliciousObject in maliciousObjects)
        {
            maliciousObject.transform.position += (collaborativeObject.position - maliciousObject.transform.position).normalized * Time.deltaTime;
        }
    }
}
