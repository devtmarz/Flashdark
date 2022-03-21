using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFinish : MonoBehaviour
{
    WorldState worldState;

    private void Start()
    {
        worldState = FindObjectOfType<WorldState>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag != "Player")
        {
            return;
        }
        worldState.LevelWin();
    }
}
