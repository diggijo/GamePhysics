using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private PlayerMovement pm;
    private const float PICKUP_DISTANCE = 1.25f;
    void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();    
    }

    void Update()
    {
        float dist = Vector3.Distance(pm.transform.position, transform.position);
        
        if(dist < PICKUP_DISTANCE)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                Destroy(gameObject);
            }
        }
    }
}
