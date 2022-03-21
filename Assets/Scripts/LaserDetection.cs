using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDetection : MonoBehaviour
{
    WorldState worldState;

    private void Start()
    {
        worldState = FindObjectOfType<WorldState>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag != "Player" || worldState.worldIsDark)
        {
            return;
        }
        worldState.LevelFail();
    }
}
