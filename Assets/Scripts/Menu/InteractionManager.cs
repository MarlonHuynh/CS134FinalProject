using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Diagnostics;

public class InteractionManager : MonoBehaviour
{
    public GoalsManager goalsManager; 

    [Header("Interactables")]
    public InteractableObject computerI;
    public InteractableObject foodSlotI; 
    public InteractableObject KitchenI; 
    public InteractableObject BedI; 
    public InteractableObject MeditationI; 
     
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


    private void Interact()
    { 
        if (lastKnownInteractable == computerI){
            _isInteracting = true; 
            interactPromptUI.SetActive(false);
            computerView.OpenComputer(); 
            pauseScreen.disablePauseScreen(); 
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
        
    }

    public IEnumerator HintCoroutine(string text, float sec){
        hintTextObj.SetActive(true); 
        hintText.text = text; 
        yield return new WaitForSeconds(sec); 
        hintTextObj.SetActive(false); 
    }

    public void ExitInteraction()
    {
        _isInteracting = false;
        interactPromptUI.SetActive(false);
        // After closing computer, enable the other overlay UIs like goals UI and pause UI
        pauseScreen.enablePauseScreen(); 
    }
}