using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaliciousObjectGeneratorYZ : MonoBehaviour
{
    public GameObject collaborativeObject; // Reference to the collaborative object.
    public GameObject maliciousObjectPrefab; // Prefab of the malicious object.
    public float translationSpeed = 2.0f; // Speed of translation.
    private GameObject maliciousObject; // The created malicious object
    private Vector3 targetPosition; // Target position of the malicious objet
    private Vector3 initialPosition ; // The initial position of the malicious object

    private float journeyLength; //  Distance between targetPosition and initialPosition
    private float startTime; // Time where the malicious object was generated
    private bool isMoving = false; // Is the malicious object moving or not

    private int maliciousObjectCount = 0; // Counter for generated malicious objects
    private int maxMaliciousObjects = 6; // Maximum number of malicious objects
    //public int randomSeed = 12356;
    public float waitTime = 1f ;
    private List<GameObject> maliciousObjects = new List<GameObject>();
    
    /*private void Start()
    {   
        //UnityEngine.Random.InitState(randomSeed);
        StartCoroutine(GenerateMaliciousObjects());
    }*/

    /// <summary>
    /// Generate a malicious object. A new object is generated every 5 seconds
    /// </summary>
    private IEnumerator GenerateMaliciousObjects()
    {
        while (maliciousObjectCount < maxMaliciousObjects)
        {
            // Generate malicious object within the bounding box of the collaborative object.
            initialPosition = GenerateRandomPositionInsideBoundingBox(collaborativeObject);

            // Instantiate the malicious object.
             maliciousObject = Instantiate(maliciousObjectPrefab, initialPosition, Quaternion.identity);
             maliciousObjects.Add(maliciousObject);
             maliciousObjectCount++;
            // Set the target position for translation.
            targetPosition = GenerateRandomPositionInsideBoundingBox(collaborativeObject);

            // Calculate the distance the malicious object needs to travel.
            journeyLength = Vector3.Distance(initialPosition, targetPosition);

            // Start moving the malicious object.
            isMoving = true;
            startTime = Time.time;

            // Wait for some time before generating the next malicious object.
            yield return new WaitForSeconds(waitTime);
        }
    }

    /// <summary>
    /// Generate a random position within the bounding boxes of a collaborative object
    /// </summary>
    /// <param name="gameObject">The collaborative object</param>
    /// <returns> The initial position of the malicious object</returns>
    
    private Vector3 GenerateRandomPositionInsideBoundingBox(GameObject gameObject)
    {
        Dictionary<string, Vector3> box = utils.GetBoundingBox(gameObject);
        Vector3 Boundmin = box["min"];
        Vector3 Boundmax = box["max"];
        // Generate random position within the bounds of the collaborative object's bounding box.
        float X = Boundmin.x + 0.5f ; 
        float randomY = Random.Range(Boundmin.y, Boundmax.y);
        float randomZ = Random.Range(Boundmin.z, Boundmax.z);; 

        return new Vector3(X, randomY, randomZ);
    }

    private void Update()
    {
        if (isMoving)
        {
            // Calculate the fraction of the journey covered so far.
            float distanceCovered = (Time.time - startTime) * translationSpeed;
            float fractionOfJourney = distanceCovered / journeyLength;

            // Interpolate the malicious object's position.
            maliciousObject.transform.position = Vector3.Lerp(initialPosition, targetPosition, fractionOfJourney);

            // Check if the malicious object has reached the destination.
            if (fractionOfJourney >= 1.0f)
            {
                isMoving = false; // Stop moving.
            }
        }
    }

    public void ResetAttack()
    {
        // Reset attack called from the agent script
        StopCoroutine(GenerateMaliciousObjects());
        if (maliciousObjects.Count > 0)
        {
            foreach (var maliciousObj in maliciousObjects)
            {
                Destroy(maliciousObj);
            }
            maliciousObjects.Clear();
            maliciousObjectCount = 0;
        } 
        StartCoroutine(GenerateMaliciousObjects());
    }
    
}
