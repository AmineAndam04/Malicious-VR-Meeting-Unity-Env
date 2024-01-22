using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaliciousCode1 : MonoBehaviour
{
    public float speed = 3f; // Adjust as needed
    public Transform targetUser; // The user you want to obstruct (assign in the Inspector)

    private void Update()
    {
        // Check if the target user is within the obstructed area based on bounding boxes
        if (IsObstructingView(targetUser))
        {
            // Continue moving towards the user or collaborative object
            MoveTowardsTarget(targetUser.position);
        }
    }

    private bool IsObstructingView(Transform user)
    {
        // Implement logic to check if the malicious object is obstructing the view
        // based on bounding boxes of the objects involved.
        // Return true if obstructing, false otherwise.
        // You may need to calculate bounding boxes and compare positions and sizes.
        // This logic depends on your specific VR environment setup.
        return false; // Placeholder logic
    }

    private void MoveTowardsTarget(Vector3 targetPosition)
    {
        // Calculate the direction to the target
        Vector3 direction = targetPosition - transform.position;

        // Normalize the direction vector
        direction.Normalize();

        // Move the malicious object towards the target
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
