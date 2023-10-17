using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private enum PlayerStates
    {
        IDLE,
        WALK,
        RUN,
        SPRINT
    }

    PlayerStates CurrentState
    {
        set
        {
            currentState = value;

            switch(currentState)
            {
                case PlayerStates.IDLE:
                    animator.SetBool("isWalking", false);
                    animator.SetBool("isRunning", false);
                    animator.SetBool("isSprinting", false);
                    break;
                case PlayerStates.WALK:
                    playerSpeed = WALK_SPEED;
                    animator.SetBool("isWalking", true);
                    break;
                case PlayerStates.RUN:
                    playerSpeed = RUN_SPEED;
                    animator.SetBool("isRunning", true);
                    animator.SetBool("isSprinting", false);
                    break;
                case PlayerStates.SPRINT:
                    playerSpeed = SPRINT_SPEED;
                    animator.SetBool("isSprinting", true);
                    break;
            }
        }
    }
    private CharacterController controller;
    private Animator animator;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed;
    private const float WALK_SPEED = 2f;
    private const float RUN_SPEED = 4f;
    private const float SPRINT_SPEED = 6f;
    private float runTimer = 0f;
    private float startRunning = 3f;
    //private float jumpHeight = 1.0f;
    //private float gravityValue = -9.81f;
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

        if(move!= Vector3.zero)
        {
            runTimer += Time.deltaTime;
            animator.SetFloat("xAxis", move.x);
            animator.SetFloat("zAxis", move.z);

            if (Input.GetKey(KeyCode.LeftShift))
            {
                CurrentState = PlayerStates.SPRINT;
            }

            else
            {
                if (runTimer < startRunning)
                {
                    CurrentState = PlayerStates.WALK;
                }

                else
                {
                    CurrentState = PlayerStates.RUN;
                }
            }
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
