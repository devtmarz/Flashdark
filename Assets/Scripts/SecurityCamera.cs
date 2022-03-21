using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    public float detectionRange = 20f;
    public bool playerDetected = false;
    Transform playerTransform;
    WorldState worldState;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        worldState = FindObjectOfType<WorldState>();
    }

    void Update()
    {
        RaycastHit hit;
        Vector3 targetDirection = playerTransform.position - transform.position;
        Physics.Raycast(transform.position, targetDirection, out hit, detectionRange);
        if (!worldState.worldIsDark && hit.transform == playerTransform)
        {
            playerDetected = true;
            Debug.DrawRay(transform.position, targetDirection * hit.distance, Color.yellow);
        }
        else
        {
            playerDetected = false;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
        }
    }
}
