using UnityEngine;

public class ComputerViewController : MonoBehaviour
{
    [Header("References")]
    public GameObject computerUI;
    public InteractionManager interactionManager;
    public MonoBehaviour playerMovement;
    public MonoBehaviour playerCamera;

    [Header("Settings")]
    public KeyCode exitKey = KeyCode.Escape;

    private bool _isOpen = false;

    void Update()
    {
        if (_isOpen && Input.GetKeyDown(exitKey))
            CloseComputer();
    }

    public void OpenComputer()
    {
        _isOpen = true;
        playerMovement.enabled = false;
        playerCamera.enabled = false;
        computerUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseComputer()
    {
        _isOpen = false;
        playerMovement.enabled = true;
        playerCamera.enabled = true;
        computerUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        interactionManager.ExitInteraction();
    }
}