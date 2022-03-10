using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashdark : MonoBehaviour
{

    WorldState worldState;
    bool _canUse;

    private void Start()
    {
        worldState = FindObjectOfType<WorldState>();
    }

    private void OnEnable()
    {
        _canUse = true;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        worldState.SetWorldDark(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && _canUse)
        {
            StartCoroutine("UsingFlashdark");
        }
    }
    IEnumerator UsingFlashdark()
    {
        _canUse = false;
        worldState.SetWorldDark(true);
        while (!Input.GetKeyUp(KeyCode.Mouse0))
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.2f);
        worldState.SetWorldDark(false);
        yield return new WaitForSeconds(0.2f);
        _canUse = true;
    }
}

/*
// Charge System

public float maxCharge;
[SerializeField] float currentCharge;

private void OnEnable()
{
    currentCharge = maxCharge;
}

IEnumerator UsingFlashdark()
{
    if (currentCharge < 1)
        yield break;
    worldState.SetWorldDark(true);
    while (currentCharge > 0 && !Input.GetKeyUp(KeyCode.Mouse0))
    {
        currentCharge -= 1 * Time.deltaTime;
        currentCharge = Mathf.Clamp(currentCharge, 0, maxCharge);
        yield return null;
    }
    worldState.SetWorldDark(false);
    yield return new WaitForSeconds(2);
    while (currentCharge < maxCharge && !Input.GetKeyDown(KeyCode.Mouse0))
    {
        currentCharge += 1 * Time.deltaTime;
        currentCharge = Mathf.Clamp(currentCharge, 0, maxCharge);
        yield return null;
    }
}
*/


