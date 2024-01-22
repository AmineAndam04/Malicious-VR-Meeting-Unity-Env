using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerChange : MonoBehaviour
{
    public Renderer render;
    public float colorfrequencyChange = 0f ;
    public float flickerfrequencyChange = 0.1f; // Adjust the flicker interval as needed

    private float minIntensity = 5f;
    private float maxIntensity = 15f;
    //private Renderer render;

    void Start()
    {
        //Renderer render = GetComponent<Renderer>();
        StartCoroutine(ChangeFlickerRoutine());
    }

    IEnumerator ChangeFlickerRoutine()
    {
        while (true)
        {
            //Color color = render.material.color;
            //color.r = Random.value; // Change the red component to a random value
            Color color = new Color(Random.value, Random.value, Random.value);
            float randomIntensity = Random.Range(minIntensity, maxIntensity);

            render.material.color = color * randomIntensity; // Apply the new color to the material

            yield return new WaitForSeconds(flickerfrequencyChange);
        }
    }
}

