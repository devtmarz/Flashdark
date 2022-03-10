using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WorldState : MonoBehaviour
{
    public GameObject voidPanel;
    public void SetWorldDark(bool dark)
    {
        voidPanel.SetActive(dark);
    }
}
