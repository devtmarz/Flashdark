using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerInteract : MonoBehaviour
{
    public Camera playerCamera;
    public float interactMaxDistance = 5f;
    TextMeshProUGUI interactText;

    private void Start()
    {
        GameObject playerOverlay = GameObject.FindGameObjectWithTag("PlayerOverlay");
        interactText = playerOverlay.GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update()
    {
        RaycastHit hit;
        Physics.Raycast(playerCamera.transform.position, Camera.main.transform.forward, out hit, interactMaxDistance);
        
        if (hit.transform == null || hit.transform.tag != "Interactable")
        {
            if (interactText.text != "")
            {
                interactText.text = "";
            }
            return;
        }
        InteractableObject io = hit.transform.GetComponent<InteractableObject>();
        if (interactText.text != io.interactText)
        {
            interactText.text = io.interactText;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            io.OnInteract();
        }
    }
}
