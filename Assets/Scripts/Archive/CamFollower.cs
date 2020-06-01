using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollower : MonoBehaviour
{
    public Transform target;

    public Vector3 offset;

    void Update()
    {
        Vector3 pos = target.position + offset;
        pos.z = -10f;

        transform.position = pos;
    }
}
