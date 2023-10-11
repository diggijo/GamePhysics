using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] public GameObject player;
    public float cameraDistance = 4f;
    public float cameraHeight = 3f;

    void LateUpdate()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + cameraHeight, player.transform.position.z - cameraDistance);
    }
}
