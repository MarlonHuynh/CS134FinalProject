using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractionManager : MonoBehaviour
{
    [Header("Settings")]
    public float interactRadius = 2.5f;
    public LayerMask interactableLayer;

    [Header("UI")]
    // [e] icon intreactable
    public GameObject interactPromptUI;
    public TextMeshProUGUI promptText;

    [Header("Computer View")]
    public ComputerViewController computerView;

    private InteractableObject _nearestInteractable;
    private bool _isInteracting = false;
    [Header("UIs to Disable upon Computer Active")]
    public PauseScreen pauseScreen; 


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
    Collider[] hits = Physics.OverlapSphere(transform.position, interactRadius, interactableLayer);

    if (hits.Length > 0)
    {
        // check the hit object AND its parent for the script??
        _nearestInteractable = hits[0].GetComponent<InteractableObject>() ?? hits[0].GetComponentInParent<InteractableObject>();

        if (_nearestInteractable != null)
        {
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
        _isInteracting = true;
        interactPromptUI.SetActive(false);
        computerView.OpenComputer();
        // After opening computer, disables the other overlay UIs like goals UI and pause UI
        pauseScreen.disablePauseScreen(); 
    }

    public void ExitInteraction()
    {
        _isInteracting = false;
        // After closing computer, enable the other otherlay UIs liek goals UI and pause UI
        pauseScreen.enablePauseScreen(); 
    }
}