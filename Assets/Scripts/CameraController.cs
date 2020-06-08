using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    CinemachineCameraOffset camOffset;
    public float lerpTime;

    float startPos;

    

    private void Awake()
    {
        #region Singleton

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Multiple instances of CameraController found. Destroying: " + gameObject.name);
            Destroy(gameObject);
        }

        #endregion

        camOffset = FindObjectOfType<CinemachineCameraOffset>();
    }



    private void Start()
    {
        startPos = camOffset.m_Offset.x;
    }

    public void FlipCamera(int dir)
    {
        StopAllCoroutines();
        StartCoroutine(LerpCameraOffset(dir));
    }

    IEnumerator LerpCameraOffset(int dir)
    {
        float start = camOffset.m_Offset.x;
        float target = startPos * dir;
        float t = 0f;

        while (!Mathf.Approximately(camOffset.m_Offset.x, target))
        {
            camOffset.m_Offset.x = Mathf.SmoothStep(start, target, t / lerpTime);

            t += Time.deltaTime;

            yield return null;
        }

        camOffset.m_Offset.x = target;

        //Debug.Log("Lerp completed");
    }
}
