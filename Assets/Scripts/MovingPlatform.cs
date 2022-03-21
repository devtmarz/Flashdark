using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private Transform playerTransform = null;
    private Vector3 offset;
    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag != "Player")
        {
            return;
        }
        playerTransform.SetParent(transform);
    }
    void OnTriggerExit(Collider col)
    {
        playerTransform.SetParent(null);
    }
}
