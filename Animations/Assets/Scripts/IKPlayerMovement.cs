using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class IKPlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;
    private const float WALK_SPEED = 1f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;


    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        animator = gameObject.GetComponentInChildren<Animator>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

            controller.Move(direction * WALK_SPEED * Time.deltaTime);
            animator.SetBool("isWalking", true);
        }

        else
        {
            animator.SetBool("isWalking", false);
        }
    }
}
