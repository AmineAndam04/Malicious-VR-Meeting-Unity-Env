using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBounds : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int t =0 ;
    private int tt =0 ;
    //private int ttt =0 ;
    void Start()
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Cube");
        Dictionary<string, Vector3> box = utils.GetBoundingBox(objectsWithTag[0]);
        Debug.Log("bb" +box["bounds"]);
        Debug.Log("min" + box["min"]);
        Debug.Log("min" + box["max"]);
        Debug.Log("Scale" + objectsWithTag[0].transform.localScale);
        BoxCollider objectCollider = objectsWithTag[0].GetComponent<BoxCollider>();
        Debug.Log("the size"+ objectCollider.size);
    }

    // Update is called once per frame
    void Update()
    {
        if (t == 1 && tt == 0)
        {
            GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Cube");
            Transform cubeTransform = objectsWithTag[0].transform;
            BoxCollider objectCollider = objectsWithTag[0].GetComponent<BoxCollider>();

            // Calculate the new scale
            Vector3 newScale = cubeTransform.localScale + new Vector3(2f,2f,2f);
            // Apply the new scale to the object
            cubeTransform.localScale = newScale;
            Debug.Log("the size"+ objectCollider.size);

            // Adjust the collider size to match the new scale
            //Vector3 newSize = objectCollider.size + new Vector3(0.5f, 0.5f, 0.5f);
            //objectCollider.size = newSize;

            // Output the updated scale and bounds
            

            tt += 1;
            
        }
        
        
        
            GameObject[] objectsWithTag1 = GameObject.FindGameObjectsWithTag("Cube");
            Transform cubeTransform1= objectsWithTag1[0].transform;
            BoxCollider objectCollider1 = objectsWithTag1[0].GetComponent<BoxCollider>();
            Debug.Log("Updated Scale: " + cubeTransform1.localScale);
            Dictionary<string, Vector3> box1 = utils.GetBoundingBox(objectsWithTag1[0]);
            Debug.Log("bb" +box1["bounds"]);
            Debug.Log("min" + box1["min"]);
            Debug.Log("min" + box1["max"]);
            
        
    }
}
