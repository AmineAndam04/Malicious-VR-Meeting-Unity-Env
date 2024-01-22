using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewBlocking : MonoBehaviour
{
    public GameObject maliciousObjectPrefab ; // The prefab of the malicious object
    public GameObject collaboObjects;
    private BoxCollider objectCollider;

    
    private void Start()
    {
        Debug.Log("Now let's test our utils functions");
        Dictionary<string, Vector3> box = utils.GetBoundingBox(collaboObjects);
        Debug.Log(box["min"]);
        Debug.Log(box["min"].x);
        Debug.Log(collaboObjects.transform.position);
    }
}

