using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testalphachange : MonoBehaviour
{
    GameObject[] objectsWithTag ;
    void Start()
    {
        objectsWithTag = GameObject.FindGameObjectsWithTag("Cube");
    }

    // Update is called once per frame
    void Update()
    {
        Material material = objectsWithTag[0].GetComponent<Renderer>().material;
        Color color = material.color;

        // Get the alpha value from the color.
        float alpha = color.a;
        Debug.Log("Alpha value is:" + alpha);
        color.a -= 0.0001f; 
        material.color = color;
        Debug.Log("Alpha value is: " + material.color.a);
    }
}
