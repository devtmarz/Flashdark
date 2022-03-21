using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InteractableObject : MonoBehaviour
{
    public string interactText = "";
    public UnityEvent interactEvents;

    public void OnInteract()
    {
        if (interactEvents != null)
            interactEvents.Invoke();
    }
}
