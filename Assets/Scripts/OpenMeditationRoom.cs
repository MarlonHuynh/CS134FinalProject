using UnityEngine;
using System.Collections;

public class OpenMeditationRoom : MonoBehaviour
{
    public GameObject door;    
    public AudioSource doorSFXSource; 
    private bool doorOpen = false; 
    private bool doorMoving = false;  
    private Vector3 closedPosition;
    private Vector3 openPosition;

    void Start(){
        closedPosition = door.transform.position; 
        openPosition = closedPosition + new Vector3(4f, 0, 0);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Z) && !doorMoving)
        {
            Debug.Log("Moving door!"); 
            if (!doorOpen)
            {
                StartCoroutine(LerpDoor(openPosition, true));
            } 
            else
            {
                StartCoroutine(LerpDoor(closedPosition, false));
            }
        } 
    }
    // Unimplemented yet: For future usage when we need to open door by script
    void openMeditationDoor()
    {
        if (!doorMoving)
        {
            if (!doorOpen)
            {
                StartCoroutine(LerpDoor(openPosition, true));
            }  
        } 
    }
    // Unimplemented yet: For future usage when we need to close door by script
    void closeMeditationDoor()
    {
        if (!doorMoving)
        {
            if (doorOpen)
            {
                StartCoroutine(LerpDoor(closedPosition, false));
            }  
        } 
    }

    IEnumerator LerpDoor(Vector3 targetPosition, bool targetState)
    {
        doorSFXSource.Play();

        doorMoving = true;
        float time = 0;
        float duration = 1f;
        Vector3 startPosition = door.transform.position;

        while (time < duration)
        {
            door.transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;  
        }

        door.transform.position = targetPosition; 
        doorOpen = targetState; 
        doorMoving = false; 
    }
}
