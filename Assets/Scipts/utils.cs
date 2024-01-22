using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public static class utils
{
    public static Dictionary<string, Vector3> GetBoundingBox(GameObject gameObject)
    {

        BoxCollider objectCollider = gameObject.GetComponent<BoxCollider>(); ;
        Dictionary<string, Vector3> boundingBox = new Dictionary<string, Vector3>();



        if (objectCollider == null)
        {
            Debug.LogError("This script requires a BoxCollider component on the GameObject.");
            return boundingBox;
        }

        // Calculate the bounding box in world space
        Bounds bounds = objectCollider.bounds;

        // Extract the min and max points of the bounding box
        Vector3 minPoint = bounds.min;
        Vector3 maxPoint = bounds.max;

        boundingBox.Add("min", minPoint);
        boundingBox.Add("max", maxPoint);
        boundingBox.Add("bounds", maxPoint - minPoint);

        return boundingBox;
    }
    public static float GetAlpha(GameObject gameObject)
    {
        Renderer objectRenderer = gameObject.GetComponent<Renderer>();
        Color objectColor = objectRenderer.material.color;
        return objectColor.a;
    }
    public static Vector2 GetFrequencies(GameObject gameObject)
    {
        ColorChange colorChangeScript;
        FlickerChange flickerChangeScript;
        colorChangeScript = gameObject.GetComponent<ColorChange>();
        flickerChangeScript = gameObject.GetComponent<FlickerChange>();
        float colorFrequency = 0f ;
        float flickerFrequency = 0f ;
        Vector2 ret = new Vector2(0f,0f) ;
        if (colorChangeScript != null)
        {
             colorFrequency = colorChangeScript.colorfrequencyChange;
             flickerFrequency = 0f;
             ret.x = 1 / colorFrequency;
             ret.y = flickerFrequency;

        }
        if (flickerChangeScript != null)
        {
            colorFrequency = 0f;
            flickerFrequency = flickerChangeScript.flickerfrequencyChange;
            ret.x = colorFrequency;
            ret.y = 1 / flickerFrequency;
        }
        //Vector2 ret ;
        //ret.x = colorFrequency;
        //ret.y = flickerFrequency;
        return ret;
    
    }

    public static Vector2 Ixyz(GameObject collab, GameObject other)
    {
        Dictionary<string, Vector3> boxCollab = utils.GetBoundingBox(collab);
        Dictionary<string, Vector3> boxOther = utils.GetBoundingBox(other);
        
        int Ix = I(new Vector2(boxCollab["max"].x,boxCollab["min"].x),new Vector2(boxOther["max"].x,boxOther["min"].x));
        int Iy = I(new Vector2(boxCollab["max"].y,boxCollab["min"].y),new Vector2(boxOther["max"].y,boxOther["min"].y));
        int Iz = I(new Vector2(boxCollab["max"].z,boxCollab["min"].z),new Vector2(boxOther["max"].z,boxOther["min"].z));

        return new Vector2(Ix*Iy,Iy*Iz);
    }

    public static Vector2 Axyz(GameObject collab, GameObject other)
    {
        Dictionary<string, Vector3> boxCollab = utils.GetBoundingBox(collab);
        Dictionary<string, Vector3> boxOther = utils.GetBoundingBox(other);
        
        float Ax = A(new Vector2(boxCollab["max"].x,boxCollab["min"].x),new Vector2(boxOther["max"].x,boxOther["min"].x));
        float Ay = A(new Vector2(boxCollab["max"].y,boxCollab["min"].y),new Vector2(boxOther["max"].y,boxOther["min"].y));
        float Az = A(new Vector2(boxCollab["max"].z,boxCollab["min"].z),new Vector2(boxOther["max"].z,boxOther["min"].z));

        return new Vector2(Ax*Ay,Ay*Az);
    }

    public static int I(Vector2 collab, Vector2 other)
    {
        
        return IndicatorCondition(collab.x,other.y) * IndicatorCondition(other.x,collab.y);
    }
    public static float A(Vector2 collab, Vector2 other)
    {
        
        return  Math.Min(collab.x,other.x) - Math.Max(collab.y,other.y);
    }
    public static int IndicatorCondition(float x, float y)
    {
        if (x > y)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
    public static float ReLU(float x)
    {
        return Math.Max(0,x);
    }

    


}
