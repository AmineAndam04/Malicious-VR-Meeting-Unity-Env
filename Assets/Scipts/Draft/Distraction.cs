using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DistractionObjectpro : MonoBehaviour
{
    public float colorChangeSpeed = 10.0f;
    public float spawnInterval = 2.0f; // Time interval to spawn new objects
    public GameObject objectPrefab;     // Prefab for the objects to spawn

    private void Start()
    {
        // Start a coroutine to spawn objects at regular intervals
        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        while (true)
        {
            // Randomly generate RGB values for the new object's color
            Color randomColor = new Color(Random.value, Random.value, Random.value);

            // Instantiate a new object and set its color
            GameObject newObject = Instantiate(objectPrefab, transform.position, Quaternion.identity);
            Renderer objectRenderer = newObject.GetComponent<Renderer>();
            if (objectRenderer != null)
            {
                objectRenderer.material.color = randomColor;
            }

            // Destroy the object after a short delay
            Destroy(newObject, 1.0f);

            // Wait for the specified spawn interval
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void Update()
    {
        // Change color and brightness over time
        Color currentColor = GetComponent<Renderer>().material.color;
        float lerpValue = Mathf.PingPong(Time.time * colorChangeSpeed, 1.0f);
        Color newColor = Color.Lerp(currentColor, currentColor * Random.Range(0.5f, 1.5f), lerpValue);
        GetComponent<Renderer>().material.color = newColor;
    }
}

/*
public class Distraction : MonoBehaviour
{
    // Start is called before the first frame update
    public float colorChangeSpeed = 1.0f ;
    private Material material ;
    void Start()
    {
        material = GetComponent<Renderer>().material ;
    }

    // Update is called once per frame
    void Update()
    {
        float lerpValue = Mathf.PingPong(Time.time * colorChangeSpeed, 1.0f);
        Color newColor = Color.Lerp(Color.red,Color.blue,lerpValue);
        material.color = newColor ;
    }
}
*/