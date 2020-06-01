using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshooter : MonoBehaviour
{
    public int num;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            string name = num.ToString() + ".png";
            ScreenCapture.CaptureScreenshot(name);
            num++;
            Debug.Log("Screenshot taken");
        }
    }
}
