using UnityEngine;
using UnityEngine.UI; 

public class GoalsCanvasInteraction : MonoBehaviour
{
    public GameObject goalsCanvas; 
 
    void Start(){
        if (goalsCanvas != null)
        {
            goalsCanvas.SetActive(true); 
        }
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            if (goalsCanvas != null)
            { 
                bool isActive = goalsCanvas.activeSelf;
                goalsCanvas.SetActive(!isActive);
            }
        }
    }
}
