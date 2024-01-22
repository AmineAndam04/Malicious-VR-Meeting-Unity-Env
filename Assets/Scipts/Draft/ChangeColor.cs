using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    public Renderer render;
    public float colorfrequencyChange = 0.5f ;
    public float flickerfrequencyChange = 0f; // Adjust the flicker interval as needed

    //private float minIntensity = 5f;
    //private float maxIntensity = 15f;
    //private Renderer render;

    void Start()
    {
        //Renderer render = GetComponent<Renderer>();
        StartCoroutine(ChangeColorRoutine());
    }

    IEnumerator ChangeColorRoutine()
    {
        while (true)
        {
            //Color color = render.material.color;
            //color.r = Random.value; // Change the red component to a random value
            Color color = new Color(Random.value, Random.value, Random.value);
            //float randomIntensity = Random.Range(minIntensity, maxIntensity);

            render.material.color = color ; // * randomIntensity; // Apply the new color to the material

            yield return new WaitForSeconds(colorfrequencyChange);
        }
    }
}

