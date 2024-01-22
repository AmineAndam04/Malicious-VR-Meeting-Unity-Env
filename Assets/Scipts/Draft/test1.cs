using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractionObjectWithSpawner : MonoBehaviour
{
    public float colorChangeSpeed = 2.0f;
    private Material material;

    private void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        // Change color and brightness over time
        float lerpValue = Mathf.PingPong(Time.time * colorChangeSpeed, 1.0f);
        Color randomColor = new Color(Random.value, Random.value, Random.value);
        material.color = Color.Lerp(material.color, randomColor, lerpValue);
    }
}
