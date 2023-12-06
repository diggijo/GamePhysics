using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBall : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    private float spawnSpeed = 5f;

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            GameObject spawnedBall = Instantiate(prefab, transform.position, Quaternion.identity);
            Rigidbody spawnedBallyRB = spawnedBall.GetComponent<Rigidbody>();
            spawnedBallyRB.velocity = transform.forward * spawnSpeed;
        }
    }
}
