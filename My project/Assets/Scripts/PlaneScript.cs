using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneScript : MonoBehaviour
{
    internal Vector3 normal;

    void Update()
    {
        normal = transform.up.normalized;
    }
}
