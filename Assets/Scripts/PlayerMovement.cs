using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    CharacterController controller;

    public float speed = 2f;
    public float gravity = 20.0f;

    private Vector3 moveDirection = Vector3.zero;
    bool isGrounded;

    public AudioClip[] activeFootstepsSound;
    public float footstepDelay;
    public AudioSource footstepsAudioSource;

    public bool moving;

    public Transform groundCheck;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    
    float x;
    float z;
    void Update()
    {
        print(gameObject.GetComponent<CharacterController>().velocity.magnitude);
        isGrounded = controller.isGrounded;
        if (isGrounded)
        {
            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");
            //z = z, -0.5f, 1f);
            // We are grounded, so recalculate
            // move direction directly from axes

            if (x != 0f || z != 0f)
            {
                moving = true;
            }
            else if (x == 0f && z == 0f)
            {
                moving = false;
            }

            if (!footstepsAudioSource.isPlaying && activeFootstepsSound.Length > 0 && moving)
            {
                footstepDelay += UnityEngine.Time.deltaTime;
                if (footstepDelay >= (0.5f + (speed * -0.1f)))
                {
                    footstepsAudioSource.clip = activeFootstepsSound[FootstepSound()];
                    footstepsAudioSource.Play();
                    footstepDelay = 0f;
                }
            }

            moveDirection = transform.right * x + transform.forward * z;
            moveDirection.Normalize();
            moveDirection *= speed;
            moveDirection.y = -2f;
        }
        else if (!isGrounded)
        {
            if (footstepsAudioSource.isPlaying)
            {
                footstepsAudioSource.Stop();
            }
            moveDirection.y -= gravity * Time.deltaTime;
        }
        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        // Move the controller
        controller.Move(moveDirection * Time.deltaTime);
    }

    int oldSound = 9999;
    int FootstepSound()
    {
        int newSound = Random.Range(0, activeFootstepsSound.Length);
        while (newSound == oldSound)
            newSound = Random.Range(0, activeFootstepsSound.Length);
        oldSound = newSound;
        return newSound;
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.tag == "CollidesWithPlayer")
        {
            Rigidbody body = hit.collider.attachedRigidbody;
            if (body != null && !body.isKinematic)
                body.velocity += hit.controller.velocity;
        }
    }
}
