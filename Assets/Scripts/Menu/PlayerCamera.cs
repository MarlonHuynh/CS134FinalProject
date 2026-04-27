using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private float _sensX = 5f;
    [SerializeField] private float _sensY = 5f;

    [SerializeField] private Transform _playerBody; 

    private float _rotationX;
 
    private void Awake()
    { 
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
    }

    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * _sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * _sensY;

        _rotationX -= mouseY;
        _rotationX = Mathf.Clamp(_rotationX, -67.5f, 67.5f);

        transform.localRotation = Quaternion.Euler(_rotationX, 0f, 0f);

        _playerBody.Rotate(Vector3.up * mouseX);
    }
 
}