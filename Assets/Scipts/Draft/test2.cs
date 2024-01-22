using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public GameObject cubePrefab;
    public float spawnInterval = 2.0f;
    public Transform userToBeDistractedPosition;

    private void Start()
    {
        // Start spawning cubes at regular intervals
        InvokeRepeating("SpawnCube", 0.0f, spawnInterval);
    }

    private void SpawnCube()
    {
        // Randomly position the cube within the specified range
        Vector3 userPosition = userToBeDistractedPosition.position;
        Vector3 objectPosition = userPosition + new Vector3(Random.Range(-5f, 5f), Random.Range(1f, 5f), Random.Range(0f, 5f));;  // userToBeDistractedPosition.forward * 2.0f;
        GameObject newCube = Instantiate(cubePrefab, objectPosition, Quaternion.identity);

        // Attach the DistractionObjectWithSpawner script to the spawned cube
        DistractionObjectWithSpawner distractionScript = newCube.GetComponent<DistractionObjectWithSpawner>();
        if (distractionScript != null)
        {
            distractionScript.colorChangeSpeed = 100000f; //Random.Range(10f, 200f); // Random color change speed
        }
    }
}
