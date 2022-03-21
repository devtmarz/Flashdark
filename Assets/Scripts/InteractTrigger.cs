using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class InteractTrigger : MonoBehaviour
{
    public UnityEvent interactEvents;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag != "Player")
        {
            return;
        }
        if (interactEvents != null)
            interactEvents.Invoke();
    }
}
