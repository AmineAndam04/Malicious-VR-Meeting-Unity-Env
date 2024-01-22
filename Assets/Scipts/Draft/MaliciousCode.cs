using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaliciousCode : MonoBehaviour
{
    public GameObject maliciousObjectPrefab; // The prefab for the malicious object.
    public Transform collaborativeObject; // Reference to the collaborative object's Transform.
    public int numberOfObjects = 5; // Number of malicious objects to generate.
    public float spawnRadius = 5.0f; // Adjust the spawn radius as needed.
    public float moveSpeed = 2.0f; // Adjust the movement speed as needed.

    private GameObject[] maliciousObjects;

    void Start()
    {
        maliciousObjects = new GameObject[numberOfObjects];

        // Instantiate multiple malicious objects.
        for (int i = 0; i < numberOfObjects; i++)
        {
            maliciousObjects[i] = Instantiate(maliciousObjectPrefab);

            // Randomly position the malicious object near the collaborative object.
            Vector3 randomOffset = Random.insideUnitSphere * spawnRadius;
            Vector3 spawnPosition = collaborativeObject.position + randomOffset;
            maliciousObjects[i].transform.position = spawnPosition;
        }
    }

    void Update()
    {
        // Move each malicious object towards the collaborative object.
        foreach (var maliciousObject in maliciousObjects)
        {
            Vector3 moveDirection = (collaborativeObject.position - maliciousObject.transform.position).normalized;
            maliciousObject.transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }
    }
}
