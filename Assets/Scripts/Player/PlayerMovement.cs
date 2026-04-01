using UnityEngine;

public class PlayerMovement: MonoBehaviour
{
    public float speed = 5f;

    private CharacterController _controller;
    private Transform _cameraTransform;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _cameraTransform = Camera.main.transform;
    }

    void Update()
    {
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