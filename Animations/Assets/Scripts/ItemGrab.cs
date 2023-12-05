using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGrab : MonoBehaviour
{
    private bool canCollect = false;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform hand;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Can pick up");
            canCollect = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Cannot pick up");
            canCollect = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            animator.SetTrigger("GrabItem");
        }
    }
}
