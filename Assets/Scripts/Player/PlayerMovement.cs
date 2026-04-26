/*

Purpose: Deals with player movement in the game 

*/
using UnityEngine;

public class PlayerMovement: MonoBehaviour
{
    public float speed = 5f; 
    private CharacterController _controller;
    private Transform _cameraTransform; 
    public bool enabledMovement; 
    public AudioClip walkingAudio; 
    public AudioSource walkingAudioSource; 

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

            // Walking SFX
            if (h != 0 || v != 0 ){
                if (walkingAudioSource.clip != walkingAudio)
                {
                    walkingAudioSource.clip = walkingAudio; 
                }
                if (walkingAudioSource.isPlaying == false)
                {
                    walkingAudioSource.Play(); 
                }
            }
            else
            {
                walkingAudioSource.clip = null;  
            }
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