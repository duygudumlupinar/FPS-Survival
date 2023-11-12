using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    private AudioSource footstepAudioSource;
    [SerializeField] AudioClip[] footstepClips;
    private CharacterController characterController;
    public float minVolume = 0.7f;
    public float maxVolume = 1f;
    private float accumulatedDistance;
    public float stepDistance = 0.1f;

    void Start()
    {
        footstepAudioSource = GetComponent<AudioSource>();
        characterController = GetComponentInParent<CharacterController>();
    }

    void Update()
    {
       makeSound();
    }
    
    void makeSound()
    {
        if (characterController.isGrounded && characterController.velocity.sqrMagnitude <= 0)
        {
            //How far the player can go until making footstep sound
            accumulatedDistance += Time.deltaTime;

            if (accumulatedDistance > stepDistance)
            {
                footstepAudioSource.volume = Random.Range(minVolume, maxVolume);
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    footstepAudioSource.clip = footstepClips[1];
                }
                else
                {
                    footstepAudioSource.clip = footstepClips[0];
                }
                footstepAudioSource.Play();

                accumulatedDistance = 0;
            }

        }
        else
        {
            accumulatedDistance = 0f;
        }
    }
}
