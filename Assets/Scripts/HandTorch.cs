using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTorch : MonoBehaviour
{
    public GameObject thrownTorch;

    [HideInInspector]
    public Vector2 torchStartPos;

    public float pointGap;
    public float power;
    Vector2 dir;
    public float torque;

    public int trajectoryPoints;
    public GameObject lineRendererPrefab;
    static GameObject lineRendererObj;
    LineRenderer lineRenderer;
    List<Vector3> points = new List<Vector3>();

    void Start()
    {
        if (!lineRendererObj)
        {
            lineRendererObj = Instantiate(lineRendererPrefab, PlayerManager.hand);
        }

        lineRenderer = lineRendererObj.GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lineRendererObj.SetActive(true);
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;
            dir =  mousePos - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x);
            SetTrajectory(angle);
            //Debug.Log(angle);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            lineRendererObj.SetActive(false);

            Throw(dir.normalized * power);
        }
    }

    void SetTrajectory(float angle)
    {
        lineRenderer.positionCount = trajectoryPoints;
        points.Clear();
        
        float t = 0f;

        for (int i = 0; i < trajectoryPoints; i++)
        {
            float x = power * t * Mathf.Cos(angle);
            float y = power * t * Mathf.Sin(angle) + (0.5f * Physics2D.gravity.y * 3 * t * t);

            points.Insert(i, new Vector3(x, y, 0));

            t += pointGap;

            //Debug.Log("Suceded");
        }

        Vector3[] array = points.ToArray();
        lineRenderer.SetPositions(array);
    }

    void Throw(Vector2 force)
    {
        Rigidbody2D obj = Instantiate(thrownTorch, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
        obj.AddForce(force, ForceMode2D.Impulse);
        obj.AddTorque(torque);

        Torch objT = obj.GetComponent<Torch>();
        objT.torchStartPos = torchStartPos;
        objT.isThrown = true;

        Destroy(gameObject);
    }
}
