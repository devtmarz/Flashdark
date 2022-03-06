using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Camera playerCamera;
    public float mouseSensitivity = 10;
    public float xAxisClamp = 0;
    float mouseXInput;
    float mouseYInput;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float camFOV = playerCamera.fieldOfView;
        mouseXInput = Input.GetAxis("Mouse X");
        mouseYInput = Input.GetAxis("Mouse Y");

        mouseXInput *= (mouseSensitivity - ((70 - playerCamera.fieldOfView) * .8f) / 6f);
        mouseYInput *= (mouseSensitivity - ((70 - playerCamera.fieldOfView) * .8f) / 6f);

        xAxisClamp += mouseYInput;

        if (xAxisClamp > 90.0f)
        {
            xAxisClamp = 90.0f;
            mouseYInput = 0.0f;
            ClampXAxisRotationToValue(270.0f);
        }
        else if (xAxisClamp < -90.0f)
        {
            xAxisClamp = -90.0f;
            mouseYInput = 0.0f;
            ClampXAxisRotationToValue(90.0f);
        }

        playerCamera.transform.Rotate(Vector3.left * mouseYInput);
        transform.Rotate(Vector3.up * mouseXInput);
        playerCamera.transform.localEulerAngles = new Vector3(playerCamera.transform.localEulerAngles.x, 0, 0);
    }

    private void ClampXAxisRotationToValue(float value)
    {
        Vector3 eulerRotation = transform.localEulerAngles;
        eulerRotation.x = value;
        playerCamera.transform.localEulerAngles = eulerRotation;
    }

    public void ResetRotation()
    {
        playerCamera.transform.localEulerAngles = new Vector3(playerCamera.transform.localEulerAngles.x, 0f, 0f);
        transform.localEulerAngles = new Vector3(0f, transform.localEulerAngles.y, 0f);
    }
}
