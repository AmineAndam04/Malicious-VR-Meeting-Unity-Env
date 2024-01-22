using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaliciousCode3 : MonoBehaviour
{
    public GameObject maliciousObjectPrefab; // The prefab for the malicious object.
    public Transform collaborativeObject; // Reference to the collaborative object's Transform.
    public int numberOfObjects = 5; // Number of malicious objects to generate.
    public float stopDistance = 1.0f; // The distance at which objects should stop.
    public float moveSpeed = 2.0f; // Adjust the movement speed as needed.

    private GameObject[] maliciousObjects;

    void Start()
    {
        maliciousObjects = new GameObject[numberOfObjects];

        // Instantiate multiple malicious objects.
        for (int i = 0; i < numberOfObjects; i++)
        {
            maliciousObjects[i] = Instantiate(maliciousObjectPrefab);

            // Randomly position the malicious objects near the collaborative object.
            Vector3 randomOffset = Random.insideUnitSphere;
            randomOffset.y = 0; // Ensure objects are at the same height as the collaborative object.
            Vector3 spawnPosition = collaborativeObject.position + randomOffset.normalized * Random.Range(1f, stopDistance);
            maliciousObjects[i].transform.position = spawnPosition;
        }
    }

    void Update()
    {
        // Move and check distance for each malicious object.
        foreach (var maliciousObject in maliciousObjects)
        {
            // Calculate the distance between the malicious object and the collaborative object.
            float distance = Vector3.Distance(maliciousObject.transform.position, collaborativeObject.position);

            // If the object is near the collaborative object, stop its movement.
            if (distance <= stopDistance)
            {
                // You can also set the rigidbody's velocity to zero if your object has one.
                Rigidbody rb = maliciousObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.velocity = Vector3.zero;
                }
            }
            else
            {
                // Move the malicious object towards the collaborative object.
                Vector3 moveDirection = (collaborativeObject.position - maliciousObject.transform.position).normalized;
                maliciousObject.transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
            }
        }
    }
}
