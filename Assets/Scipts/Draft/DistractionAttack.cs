using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractionObjectWithSpawner2 : MonoBehaviour
{
    public float colorChangeSpeed = 1.0f;
    public float spawnInterval = 2.0f;
    public GameObject cubePrefab;
    public Transform userToBeDistractedPosition ;
    

    private Material material;

    private void Start()
    {
        material = GetComponent<Renderer>().material;

        // Start spawning cubes at regular intervals
        InvokeRepeating("SpawnCube", 0.0f, spawnInterval);
    }

    private void Update()
    {
        // Change color and brightness over time
        float lerpValue = Mathf.PingPong(Time.time * colorChangeSpeed, 1.0f);
        Color randomColor = new Color(Random.value, Random.value, Random.value);
        material.color = Color.Lerp(material.color, randomColor, lerpValue);
    }

    private void SpawnCube()
    {
        // Randomly position the cube within the specified range
        Vector3 userPosition = userToBeDistractedPosition.position;
        Vector3 objectPosition = userPosition + new Vector3(Random.Range(-5f, 5f), Random.Range(1f, 5f), Random.Range(0f, 5f));;  // userToBeDistractedPosition.forward * 2.0f;
        Vector3 randomPosition = new Vector3(Random.Range(-5f, 5f), Random.Range(1f, 5f), Random.Range(-5f, 5f));
        GameObject newCube = Instantiate(cubePrefab, objectPosition, Quaternion.identity);

        // Attach the DistractionObjectWithSpawner script to the spawned cube
        DistractionObjectWithSpawner2 distractionScript = newCube.GetComponent<DistractionObjectWithSpawner2>();
        if (distractionScript != null)
        {
            distractionScript.colorChangeSpeed = Random.Range(10f, 20f); // Random color change speed
        }
    }
}

