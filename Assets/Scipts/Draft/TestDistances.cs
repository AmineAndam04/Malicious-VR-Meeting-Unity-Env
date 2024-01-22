using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDistances : MonoBehaviour
{
    public GameObject box ;
    public GameObject avatar ;
    private float distance ;
    

    // Update is called once per frame
    void Update()
    {
         distance =  Vector3.Distance(box.transform.localPosition,avatar.transform.localPosition);
         Debug.Log("Distance: " + distance);
    }
}
