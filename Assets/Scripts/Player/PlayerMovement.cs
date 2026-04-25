using UnityEngine;

public class PlayerMovement: MonoBehaviour
{
    public float speed = 5f;

    private CharacterController _controller;
    private Transform _cameraTransform;

    public bool enabledMovement; 

    void Start()
    {
        enabledMovement = true; 
        _controller = GetComponent<CharacterController>();
        _cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        if (enabledMovement == true){
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            Vector3 camForward = _cameraTransform.forward;
            Vector3 camRight = _cameraTransform.right;
            camForward.y = 0f;
            camRight.y = 0f;
            camForward.Normalize();
            camRight.Normalize();

            Vector3 move = camForward * v + camRight * h;
            _controller.Move(move * speed * Time.deltaTime);
        }
    }

    public void disableMovement()
    {
        enabledMovement = false; 
    }
    
    public void enableMovement()
    {
        enabledMovement = true; 
    }
}