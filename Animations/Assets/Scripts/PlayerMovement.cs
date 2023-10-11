using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private enum PlayerStates
    {
        IDLE,
        WALK,
        RUN
    }

    PlayerStates CurrentState
    {
        set
        {
            currentState = value;

            switch(currentState)
            {
                case PlayerStates.IDLE:
                    animator.Play("Idle");
                    break;
                case PlayerStates.WALK:
                    animator.Play("Walk");
                    break;
                case PlayerStates.RUN:
                    animator.Play("Run");
                    break;
            }
        }
    }
    private CharacterController controller;
    private Animator animator;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 2.0f;
    private float runTimer = 0f;
    private float startRunning = 3f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    private PlayerStates currentState;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        animator = gameObject.GetComponentInChildren<Animator>();
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);

        Debug.Log(move);
        if (move != Vector3.zero && runTimer < startRunning)
        {
            CurrentState = PlayerStates.WALK;
            animator.SetFloat("xAxis", move.x);
            animator.SetFloat("zAxis", move.z);
            runTimer += Time.deltaTime;
        }
        else if(move!= Vector3.zero && runTimer > startRunning)
        {
            CurrentState = PlayerStates.RUN;
            animator.SetFloat("xAxis", move.x);
            animator.SetFloat("zAxis", move.z);
        }
        else
        {
            CurrentState = PlayerStates.IDLE;
            runTimer = 0;
        }

        /*
        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);*/
    }
}
