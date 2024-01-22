using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
// Data  structures to store data 
public struct CollabObject
{
    public GameObject gameObject;

    public CollabObject(GameObject gameObject)
    {
        this.gameObject = gameObject;
    }

    public Vector3 GetPosition()
    {
        return gameObject.transform.localPosition;
        
    }
    public Dictionary<string, Vector3>  GetBB()
    {
        Dictionary<string, Vector3> box = utils.GetBoundingBox(gameObject);
        return box; //box["bounds"];
    }

}

public struct ObscObject
{
    public GameObject gameObject;

    public ObscObject(GameObject gameObject)
    {
        this.gameObject = gameObject;
    }

    public Vector3 GetPosition()
    {
        return gameObject.transform.localPosition;
        
    }
    public Dictionary<string, Vector3>  GetBB()
    {
        Dictionary<string, Vector3> box = utils.GetBoundingBox(gameObject);
        return box; //box["bounds"];
    }

    public float GetAlpha()
    {
        float alpha = utils.GetAlpha(gameObject);
        return alpha;
    }
     public void ShiftPosition(Vector3 shift)
     {
        gameObject.transform.localPosition += shift;
     }
     public void ShiftBB(Vector3 shift)
     {
        BoxCollider objectCollider = gameObject.GetComponent<BoxCollider>();
        Vector3 newScale = gameObject.transform.localScale + shift;
        gameObject.transform.localScale = newScale;
     }
     public void ShiftAlpha(float shift)
     {
        Material material = gameObject.GetComponent<Renderer>().material;
        Color color = material.color;
        color.a += shift; 
        material.color = color;
     }
}


public struct DistrcObject
{
    public GameObject gameObject;

    public DistrcObject(GameObject gameObject)
    {
        this.gameObject = gameObject;
    }

    public Vector3 GetPosition()
    {
        return gameObject.transform.localPosition;
        
    }
    public Vector3 GetBB()
    {
        Dictionary<string, Vector3> box = utils.GetBoundingBox(gameObject);
        return box["bounds"];
    }
    public float GetVolume()
    {
        Vector3 bb = this.GetBB();
        return bb.x * bb.y * bb.z ;
    }

    public float GetAlpha()
    {
        float alpha = utils.GetAlpha(gameObject);
        return alpha;
    }
    public Vector2 Getfreq()
    {
        Vector2 freq = utils.GetFrequencies(gameObject);
        //this.freqs = freq;
        return freq;
    }
    public void ShiftBB(Vector3 shift)
     {
        BoxCollider objectCollider = gameObject.GetComponent<BoxCollider>();
        Vector3 newScale = gameObject.transform.localScale + shift;
        gameObject.transform.localScale = newScale;
     }
     public void ShiftFreq(Vector2 shift)
     {
        
        
        Vector2 freqs = this.Getfreq();
        Vector2 realShift = new Vector2(0f,0f);
        realShift.x = (-shift.x)/ (freqs.x*freqs.x + freqs.x*shift.x);
        realShift.y = (-shift.y)/ (freqs.y*freqs.y + freqs.y*shift.y);
        ColorChange colorChangeScript;
        FlickerChange flickerChangeScript;
        colorChangeScript = gameObject.GetComponent<ColorChange>();
        flickerChangeScript = gameObject.GetComponent<FlickerChange>();
        if (colorChangeScript != null)
        {
            colorChangeScript.colorfrequencyChange += realShift.x;
        }
        if (flickerChangeScript != null)
        {
            flickerChangeScript.flickerfrequencyChange += realShift.y;
        }
     }
}

public struct Avatar
{
    public GameObject gameObject;

    public Avatar(GameObject gameObject)
    {
        this.gameObject = gameObject;
    }
    public Vector3 GetPosition()
    {
        return gameObject.transform.localPosition;
        
    }
}

public struct MaliciuosAvatar
{
    public GameObject gameObject;

    public MaliciuosAvatar(GameObject gameObject)
    {
        this.gameObject = gameObject;
    }
    public Vector3 GetPosition()
    {
        return gameObject.transform.localPosition;
        
    }
    public void ShiftPosition(Vector3 shift)
     {
        gameObject.transform.localPosition += shift;
     }
    
}
