/*

Purpose: Handles all non-computer (in-room) interactions in the game 

*/
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Diagnostics;

public class InteractionManager : MonoBehaviour
{
    [Header("Refs")]
    public GoalsManager goalsManager; 
    public EndingCutsceneManager endingCutsceneManager; 
    public PlayerMovement playerMovement; 
    public GoalsCanvasInteraction goalsCanvasInteraction; 

    [Header("Interactables")]
    public InteractableObject computerI;
    public InteractableObject foodSlotI; 
    public InteractableObject KitchenI; 
    public InteractableObject BedI; 
    public InteractableObject MeditationI; 
    public InteractableObject MeditationDoorHintI; 
    public InteractableObject MetalDoorHintI; 
     
    [Header("Settings")]
    public float interactRadius = 2.5f;
    public LayerMask interactableLayer;

    [Header("UI")]
    // [e] icon intreactable
    public GameObject hintTextObj; 
    public TextMeshProUGUI hintText; 
    public GameObject interactPromptUI;
    public TextMeshProUGUI promptText;

    [Header("Computer View")]
    public ComputerViewController computerView;
    private InteractableObject _nearestInteractable;
    private bool _isInteracting = false;

    [Header("UIs to Disable upon Computer Active")]
    public PauseScreen pauseScreen; 

    // Storage of closest Interactable
    private InteractableObject lastKnownInteractable; 
    public bool disableOtherInteractablesBesidesMetalDoor; 


    void Start()
    {
        disableOtherInteractablesBesidesMetalDoor = false; 
    }
    void Update()
    {
        if (_isInteracting) return;

        DetectInteractable();

        if (_nearestInteractable != null && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    private void DetectInteractable()
    {
        // Clears interactables every frame so it can detect if you step away from collider 
        _nearestInteractable = null;
        interactPromptUI.SetActive(false);

        Collider[] hits = Physics.OverlapSphere(transform.position, interactRadius, interactableLayer);

        if (hits.Length > 0)
        {
            // Check hit object for Interactables script
            _nearestInteractable = hits[0].GetComponent<InteractableObject>() ?? hits[0].GetComponentInParent<InteractableObject>();

            if (_nearestInteractable != null)
            {
                lastKnownInteractable = _nearestInteractable; 
                interactPromptUI.SetActive(true);
                if (promptText != null)
                    promptText.text = _nearestInteractable.promptText;
            }
        }
        else
        {
            _nearestInteractable = null;
            interactPromptUI.SetActive(false);
        }
    }

    // Define interaction logic 
    private void Interact()
    { 
        // Used for ending 1
        if (disableOtherInteractablesBesidesMetalDoor == true){
            if (lastKnownInteractable != MetalDoorHintI)
            {
                StartCoroutine(HintCoroutine("Answer the door.", 3f));  
                return; 
            }
            else
            {
                // Trigger Meatgrinder ending
                endingCutsceneManager.playEnding(1); 
                return; 
            }
        }

        // Normal behavior of interactables in the room
        if (lastKnownInteractable == computerI){
            _isInteracting = true; 
            interactPromptUI.SetActive(false);
            computerView.OpenComputer(); 
            pauseScreen.disablePauseScreen(); 
            playerMovement.disableMovement(); 
            playerMovement.walkingAudioSource.Stop(); 
            goalsCanvasInteraction.disableGoalsCanvas(); 
            
        }
        else if (lastKnownInteractable == BedI){ 
            goalsManager.checkIfTasksCompletedAndSleep();  
        }
        else if (lastKnownInteractable == MeditationI){ 
            goalsManager.meditate(); 
        }
        else if (lastKnownInteractable == foodSlotI){ 
            bool foodSlotOpen = goalsManager.getFood();  
            if (!foodSlotOpen){
                StartCoroutine(HintCoroutine("You need to purchase food first...", 3f)); 
            }
        }
        else if (lastKnownInteractable == KitchenI){ 
            if (goalsManager.holdingFood){
                goalsManager.consumeFood();  
            }
            else
            {
                StartCoroutine(HintCoroutine("The perfect spot to eat your nutritional paste, if you had any.", 3f)); 
            }
        }
        else if (lastKnownInteractable == MeditationDoorHintI){  
            StartCoroutine(HintCoroutine("You need to purchase access to meditation room.", 3f));  
        }
        else if (lastKnownInteractable == MetalDoorHintI){  
            StartCoroutine(HintCoroutine("The metal door is locked.", 3f));  
        }
        
    }

    // Displays a hint if needed 
    public IEnumerator HintCoroutine(string text, float sec){
        hintTextObj.SetActive(true); 
        hintText.text = text; 
        yield return new WaitForSeconds(sec); 
        hintTextObj.SetActive(false); 
    }

    // Exiting interaction radius logic 
    public void ExitInteraction()
    {
        _isInteracting = false;
        interactPromptUI.SetActive(false);
        // After closing computer, enable the other overlay UIs like goals UI and pause UI
        pauseScreen.enablePauseScreen(); 
        playerMovement.enableMovement(); 
        goalsCanvasInteraction.enableGoalsCanvas(); 
    }
}