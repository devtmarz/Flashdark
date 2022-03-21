using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldState : MonoBehaviour
{
    public GameObject voidPanel;
    public Camera mainCamera;
    public bool worldIsDark = false;
    public GameObject facilityAlarm;
    public bool failed = false;
    GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void SetWorldDark(bool dark)
    {
        StartCoroutine(ToggleVisualDarkness(dark));
    }

    public void LevelWin()
    {
        if (failed == true)
        {
            return;
        }
        StopAllCoroutines();
        StartCoroutine(LevelWinEnum());
    }

    IEnumerator LevelWinEnum()
    {
        yield return StartCoroutine(ToggleVisualDarkness(false));
        worldIsDark = true;
        yield return new WaitForSecondsRealtime(3f);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LevelFail()
    {
        StopAllCoroutines();
        failed = true;
        StartCoroutine(LevelFailEnum());
    }

    IEnumerator LevelFailEnum()
    {
        yield return StartCoroutine(ToggleVisualDarkness(false));
        worldIsDark = true;
        facilityAlarm.SetActive(true);
        yield return new WaitForSecondsRealtime(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator ToggleVisualDarkness(bool dark)
    {
        float value;
        float increment;
        if (dark)
        {
            value = 0.01f;
            increment = -5f;
        }
        else
        {
            value = 50f;
            increment = 5f;
        }

        while (RenderSettings.fogEndDistance != value)
        {
            float fogDensityValue = RenderSettings.fogEndDistance + increment;
            RenderSettings.fogEndDistance = Mathf.Clamp(fogDensityValue, 0.01f, 50f);
            yield return new WaitForFixedUpdate();
        }
        worldIsDark = dark;
        yield return null;
    }
}
