/*

Purpose: Turns the goals canvas on and off based on player input 

*/

using UnityEngine;
using UnityEngine.UI; 

public class GoalsCanvasInteraction : MonoBehaviour
{
    public GameObject goalsCanvas; 
    public bool goalsCanvasEnabled; 
 
    void Start(){
        goalsCanvasEnabled = true; 
        if (goalsCanvas != null)
        {
            goalsCanvas.SetActive(true); 
        }
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F) && goalsCanvasEnabled)
        {
            if (goalsCanvas != null)
            { 
                bool isActive = goalsCanvas.activeSelf;
                goalsCanvas.SetActive(!isActive);
            }
        }
    }

    public void disableGoalsCanvas()
    {
        goalsCanvasEnabled = false; 
        goalsCanvas.SetActive(false);
    }

    public void enableGoalsCanvas()
    {
        goalsCanvasEnabled = true; 
    }
}
