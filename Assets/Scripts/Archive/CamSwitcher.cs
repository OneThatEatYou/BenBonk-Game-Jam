using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSwitcher : MonoBehaviour
{
    public Camera newCam;

    Camera curCam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (newCam == curCam)
        {
            return;
        }

        curCam = Camera.current;

        curCam.enabled = false;
        newCam.enabled = true;
    }
}
