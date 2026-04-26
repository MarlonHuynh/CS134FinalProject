/*

Purpose: Deals with logic regarding opening the meditation room door.

*/
using UnityEngine;
using System.Collections;

public class OpenMeditationRoom : MonoBehaviour
{
    [Header("Refs")]
    public GameObject hint; 
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
        /*
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
        } */
    }
    // Opens the door
    public void openMeditationDoor()
    {
        if (!doorMoving)
        {
            if (!doorOpen)
            {
                StartCoroutine(LerpDoor(openPosition, true));
                hint.SetActive(false); 
            }  
        } 
    }
    // Closes the door (unused)
    void closeMeditationDoor()
    {
        if (!doorMoving)
        {
            if (doorOpen)
            {
                StartCoroutine(LerpDoor(closedPosition, false));
                hint.SetActive(true); 
            }  
        } 
    }
    // Closes the door instantly without lerping
    public void closeMeditationDoorImmediately()
    {
        door.transform.position = closedPosition; 
        doorOpen = false; 
        doorMoving = false;  
        hint.SetActive(true); 
    }

    // Slowly moves door
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
