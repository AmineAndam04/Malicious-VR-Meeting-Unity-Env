using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testactioninsphere : MonoBehaviour
{
    public GameObject shpere ;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Dictionary<string, Vector3> box = utils.GetBoundingBox(shpere);
        float volume = box["bounds"].x * box["bounds"].y * box["bounds"].z;
        Debug.Log("Volume:" + volume);
        BoxCollider objectCollider = shpere.GetComponent<BoxCollider>();
        Vector3 shift = new Vector3(-0.03f,-0.03f,-0.03f);
        Vector3 newScale = shpere.transform.localScale + shift;
        
        shpere.transform.localScale = newScale;
    }
}
