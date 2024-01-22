using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatePrefab : MonoBehaviour
{
    public GameObject prefabOfColor;
    public GameObject prefabOfFlicker ;
    public Transform userToBeDistractedPosition ;
    public int maxDistractionObject = 6 ;
    private int countObjects =0 ;
    //public int randomSeed = 12678;
    //private int countColor = 0;
    //private int countFlicker = 0;

    public float generatPeriod = 5f;
    private List<GameObject> distractionObjects = new List<GameObject>();

    
    /*private void Start()
    {
        //UnityEngine.Random.InitState(randomSeed);
        StartCoroutine(InstantiatePrefabRoutine());
    }*/

    IEnumerator InstantiatePrefabRoutine()
    {
        while (countObjects < maxDistractionObject)
        {
            
            Vector3 userPosition = userToBeDistractedPosition.position;
            Vector3 objectPosition1 = new Vector3(Random.Range(-6f, 9f), Random.Range(3f, 9f), Random.Range(19f, 25f));;  // userToBeDistractedPosition.forward * 2.0f;
            Vector3 objectPosition2 = new Vector3(Random.Range(-6f, 9f), Random.Range(3f, 9f), Random.Range(19f, 25f));
            //int choice = Random.Range(0, 2);
            //if (countColor == 3){choice = 0;}
            //if (countFlicker == 3){choice = 1;}
            GameObject distractionObject1;
            GameObject distractionObject2;
            distractionObject1 = Instantiate(prefabOfColor, objectPosition1, Quaternion.identity);
            countObjects++;
                //countColor++ ;
                //Debug.Log("Color: " + countColor);
            distractionObject2 = Instantiate(prefabOfFlicker, objectPosition2, Quaternion.identity);
            countObjects++;
                //countFlicker++;
                //Debug.Log("flicker: " + countFlicker);
            //}
            distractionObjects.Add(distractionObject1);
            distractionObjects.Add(distractionObject2);
            // Instantiate the prefab at the position of the empty GameObject
            // Instantiate(prefabToInstantiate, objectPosition, Quaternion.identity);
            // countObjects++;
            //Instantiate(prefabToInstantiate, transform.position, Quaternion.identity);

            yield return new WaitForSeconds(generatPeriod);
        }
    }
    public void ResetAttack()
    {
        StopCoroutine(InstantiatePrefabRoutine());
        if (distractionObjects.Count >0)
        {
            foreach (var distractionObject in distractionObjects)
            {
                    Destroy(distractionObject);
            }
            distractionObjects.Clear();
            countObjects = 0;
        }
        StartCoroutine(InstantiatePrefabRoutine());
        
    }
}
