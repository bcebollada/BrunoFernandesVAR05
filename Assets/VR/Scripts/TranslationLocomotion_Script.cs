using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslationLocomotion_Script : MonoBehaviour
{
    private RagnarokVRInputController input;
    public Transform head;

    private bool isReproducingWalingSound;

    private AudioSource audioSource;
    public AudioClip walkingSound;


    private void Awake()
    {
        input = GetComponent<RagnarokVRInputController>();

        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
    }


    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = input.JoystickLeft;

        //Convert moveDirection from local to world space

        Vector3 forward = head.forward;
        forward.y = 0;
        forward = forward.normalized;

        Vector3 right = head.right;
        right.y = 0;
        right = right.normalized;

        Vector3 moveDirection = forward * moveInput.y + right * moveInput.x;

        transform.position += moveDirection * Time.deltaTime;

        PlayWalkingSound(moveInput.magnitude);
    }

    private void PlayWalkingSound(float moveInputMag)
    {
        if (moveInputMag > 0.1f)
        {
            if (isReproducingWalingSound) return;
            Debug.Log("ReproducingWalkingSound");

            isReproducingWalingSound = true;
            audioSource.clip = walkingSound;
            audioSource.Play();
        }
        else
        {
            isReproducingWalingSound = false ;
            audioSource.Stop();
        }



    }
}
