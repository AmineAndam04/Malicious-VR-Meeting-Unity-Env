using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereColorChange : MonoBehaviour
{
    public float privateSpace = 1.5f; 
    
    private Renderer sphereRenderer ;
    private float checkInterval = 0.0f;
    private Color originalColor;
    private void Awake() {
        Transform privateSpaceSphere = transform.Find("PrivateSpace"); 
        sphereRenderer = privateSpaceSphere.GetComponent<Renderer>();
        originalColor = sphereRenderer.material.color; 
        StartCoroutine(CheckDistanceRoutine());
    }

    private void duc()
    {
        bool isMaliciousAvatarClose = false;
        GameObject[] maliciousAvatars = GameObject.FindGameObjectsWithTag("AvatarMalicious");

        foreach (GameObject maliciousAvatar in maliciousAvatars)
        {
            if (Vector3.Distance(transform.position, maliciousAvatar.transform.position) < privateSpace)
            {
                isMaliciousAvatarClose = true;
                Debug.Log("Here");
                break;
            }
        }

        if (isMaliciousAvatarClose)
        {
            sphereRenderer.material.color = new Color(1f, 0f, 0f, sphereRenderer.material.color.a);
            Debug.Log("And here of course");
        }
        else
        {
             sphereRenderer.material.color = originalColor;
        }
        }
        private IEnumerator CheckDistanceRoutine()
    {
        while (true)
        {
            bool isMaliciousAvatarClose = false;
        GameObject[] maliciousAvatars = GameObject.FindGameObjectsWithTag("AvatarMalicious");

        foreach (GameObject maliciousAvatar in maliciousAvatars)
        {
            if (Vector3.Distance(transform.position, maliciousAvatar.transform.position) < privateSpace)
            {
                isMaliciousAvatarClose = true;
                Debug.Log("Here");
                break;
            }
        }

        if (isMaliciousAvatarClose)
        {
            sphereRenderer.material.color = new Color(1f, 0f, 0f, sphereRenderer.material.color.a);
            Debug.Log("And here of course");
        }
        else
        {
             sphereRenderer.material.color = originalColor;
        }
            yield return new WaitForSeconds(checkInterval); 
    }
}}

    