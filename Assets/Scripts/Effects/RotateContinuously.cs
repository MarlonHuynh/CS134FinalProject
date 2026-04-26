/*

Purpose: Rotates object in the z direction
Note: Used for the dodecahedron in the title screen

*/

using UnityEngine;

public class RotateContinuously : MonoBehaviour
{
    public float rotationSpeed = 10f;  

    void Update()
    { 
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}
