using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestTags : MonoBehaviour
{
    private  string[] tagsToFind = {"collaborative","Obscure","Distraction","Avatar","AvatarMalicious"} ;

     void Start() {
        /*GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("collaborative");
        Dictionary<string, Vector3> box = utils.GetBoundingBox(objectsWithTag[0]);
        Vector3 Boundmin = box["min"];
        Vector3 Boundmax = box["max"];
        Debug.Log("Min: " + Boundmin);
        Debug.Log("Max: " + Boundmax);
        Vector3 bounds = Boundmax - Boundmin;
        Debug.Log("Bounds: " + bounds);
        Debug.Log("Bounds with function " +box["bounds"]);
        Debug.Log("Bound on x: "+ bounds.x);
        Debug.Log("Bound on x: "+ bounds.y);
        Debug.Log("Bound on x: "+ bounds.z);*/
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("collaborative");
        CollabObject customData = new CollabObject(objectsWithTag[0]);
        Debug.Log(customData.GetPosition());
        
    }
    void Update()
    {
       /* foreach (string tag in tagsToFind)
        {
            GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);
            Debug.Log("the number of " + tag + "is = " + objectsWithTag.Length);         
        }
        Debug.Log("THE END OF AN UPDATE");*/
         //Get the alpha value
         //GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Obscure");
         /*Material material = objectsWithTag[0].GetComponent<Renderer>().material;
         Debug.Log("position:" + objectsWithTag[0].transform.position);
        // Get the material's color.
        Color color = material.color;

        // Get the alpha value from the color.
        float alpha = color.a;
        color.a = 1.0f; 
        objectsWithTag[0].material.color = color;
        //Debug.Log("Alpha value is: " + alpha);*/
        /*Renderer objectRenderer = objectsWithTag[0].GetComponent<Renderer>();
        Color objectColor = objectRenderer.material.color;
        
        Debug.Log("Without function " + objectColor.a);
        Debug.Log("With function " + utils.GetAlpha(objectsWithTag[0]));*/
        /*GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Distraction");
        ColorChange colorChangeScript;
        colorChangeScript = objectsWithTag[0].GetComponent<ColorChange>();
        
    // Access the colorfrequencyChange variable
        float colorFrequency = colorChangeScript.colorfrequencyChange;
        Debug.Log("The frequency is: "+ colorFrequency);
        //colorChangeScript.colorfrequencyChange= 0.2f; 
        Debug.Log("With function we get: " + utils.GetFrequencies(objectsWithTag[0]));*/

    }
}
