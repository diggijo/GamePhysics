using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGrab : MonoBehaviour
{
    private bool canCollect = false;
    [SerializeField] Animator animator;

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
        if (Input.GetKeyDown(KeyCode.E) && canCollect)
        {
            animator.SetTrigger("GrabItem");
            Destroy(gameObject);
        }
    }
}
