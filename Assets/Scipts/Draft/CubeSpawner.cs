using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner1 : MonoBehaviour
{
    public GameObject cubePrefab;
    public float spawnInterval = 1.0f;

    private void Start()
    {
        // Start spawning cubes at regular intervals
        InvokeRepeating("fSpawnCube", 0.0f, spawnInterval);
    }

    private void fSpawnCube()
    {
        // Randomly position the cube within the specified range
        Vector3 randomPosition = new Vector3(Random.Range(-5f, 5f), Random.Range(1f, 5f), Random.Range(-5f, 5f));
        Instantiate(cubePrefab, randomPosition, Quaternion.identity);
    }
}

