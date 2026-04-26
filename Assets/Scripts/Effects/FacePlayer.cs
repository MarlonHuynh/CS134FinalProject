/*

Purpose: Makes the current object face the player

*/
using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    public Transform playerCamera; 
    void LateUpdate()
    {
        if (playerCamera == null) return;

        // Make the canvas face the camera
        transform.LookAt(transform.position + playerCamera.forward);

        // Keep it upright (no tilting)
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
    }
}