using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectColorChanger : MonoBehaviour
{
    public GameObject objectPrefab; // Assign the object prefab you want to change color in the Inspector
    public float colorChangeInterval = 5.0f; // Time interval for color change in seconds

    private GameObject objectToChangeColor;
    private Renderer objectRenderer;

    private void Start()
    {
        // Instantiate the object from the prefab
        objectToChangeColor = Instantiate(objectPrefab, transform.position, Quaternion.identity);

        // Access the Renderer component of the instantiated object
        objectRenderer = objectToChangeColor.GetComponent<Renderer>();

        // Start the coroutine for color changing
        StartCoroutine(ChangeColorRoutine());
    }

    private IEnumerator ChangeColorRoutine()
{
    while (true)
    {
        // Change color and brightness
        Color randomColor = new Color(Random.value, Random.value, Random.value);
        objectRenderer.material.color = randomColor;

        // Wait for the specified colorChangeInterval
        yield return new WaitForSeconds(colorChangeInterval);
    }
}
}
