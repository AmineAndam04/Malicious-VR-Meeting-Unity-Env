using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UserHarassmentSim : MonoBehaviour
{
    public GameObject maliciousAvatarPrefab;
    private GameObject maliciousAvatar;
    private float spawnDelay = 1f;
    private Vector3 initialPosition = new Vector3(16f, 4.2f, 31f);

    private GameObject[] otherAvatars;
    public float privateSpace = 2.5f;
    private GameObject targetedAvatar;
    public float attackPeriod = 180f; // it was 50s
    public int attackCount = 0;
    private GameObject previousTargetedAvatar;

    void Start()
    {
        //Invoke("SpawnMaliciousAvatar", spawnDelay);
        otherAvatars = GameObject.FindGameObjectsWithTag("Avatar");
    }

    void SpawnMaliciousAvatar()
    {
        
        if (maliciousAvatar == null)
        {
            maliciousAvatar = Instantiate(maliciousAvatarPrefab, initialPosition, Quaternion.identity);
        }
        
        StartCoroutine(AttackRoutine());
        
        
    }

    IEnumerator AttackRoutine()
    {
        
        while (maliciousAvatar != null )
        {
            
        
            //Debug.Log(attackCount);
            // Randomly choose a new targeted avatar only when the previous attack is over.
            if (targetedAvatar == null)
            {
                do
                {
                    targetedAvatar = otherAvatars[Random.Range(0, otherAvatars.Length)];
                } while (targetedAvatar == previousTargetedAvatar); //  To ensure the new target is not the same as the previous one

                //Debug.Log("New Target: " + targetedAvatar.transform.position);
                //Debug.Log("Distance btw them is:" + Vector3.Distance(maliciousAvatar.transform.position,targetedAvatar.transform.position));
                previousTargetedAvatar = targetedAvatar;
                maliciousAvatar.transform.position = Vector3.Lerp(maliciousAvatar.transform.position, targetedAvatar.transform.position, 0.92f);
                attackCount++;
            }

            float distance = Vector3.Distance(maliciousAvatar.transform.position, targetedAvatar.transform.position);
            

            if (distance <    privateSpace)
            {
                
                // Attack the target avatar.
                Transform privateSpaceSphere = targetedAvatar.transform.Find("PrivateSpace"); 
                Renderer sphereRenderer = privateSpaceSphere.GetComponent<Renderer>();
                Color originalColor = sphereRenderer.material.color; 
                sphereRenderer.material.color = new Color(1f, 0f, 0f, sphereRenderer.material.color.a);
                yield return new WaitForSeconds(0.5f);
                sphereRenderer.material.color = originalColor ; 
                // Reset the targeted avatar to null to choose a new target in the next attack.
                targetedAvatar = null;
                //sphereRenderer.material.color = originalColor ;
                yield return new WaitForSeconds(attackPeriod);
                
            }
            else
            {
                //maliciousAvatar.transform.position = Vector3.Lerp(maliciousAvatar.transform.position, targetedAvatar.transform.position, Time.deltaTime);
                //Debug.Log("Distance" + distance);
            }
            //Debug.Log(attackCount);
            yield return null;
        
        }
    }

    public void ResetAttack()
    {
        attackCount = 0;
        GameObject[] maliciousAvatars = GameObject.FindGameObjectsWithTag("AvatarMalicious");
        if (maliciousAvatars.Length >0 )
        {
            StopCoroutine(AttackRoutine());
            foreach (GameObject malAvatar in maliciousAvatars)
            {
                Destroy(malAvatar);
            }
        }
        targetedAvatar = null;
        previousTargetedAvatar = null;
        foreach (GameObject  avatar in otherAvatars)
        {
            Transform privateSpaceSphere = avatar.transform.Find("PrivateSpace"); 
            Renderer sphereRenderer = privateSpaceSphere.GetComponent<Renderer>(); 
            sphereRenderer.material.color = new Color(0.299f, 0.898f, 0.294f, sphereRenderer.material.color.a);
        }
        
        Invoke("SpawnMaliciousAvatar", spawnDelay);
    }
}

