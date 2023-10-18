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
        SPRINT,
        JUMP
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
                case PlayerStates.JUMP:
                    animator.SetBool("isJumping", true);
                    break;
            }
        }
    }
    private PlayerStates currentState;
    private CharacterController controller;
    private Animator animator;
    private float playerSpeed;
    private const float WALK_SPEED = 2f;
    private const float RUN_SPEED = 4f;
    private const float SPRINT_SPEED = 6f;
    private float runTimer = 0f;
    private float startRunning = 3f;
    private Vector3 velocity;
    private float jumpHeight = 3f;
    private float gravity = -9.81f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Transform groundCheck;
    private float groundDistance = .05f;
    private bool isGrounded;


    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        animator = gameObject.GetComponentInChildren<Animator>();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
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
            if (isGrounded)
            {
                CurrentState = PlayerStates.IDLE;
            }

            runTimer = 0;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravity);
            CurrentState = PlayerStates.JUMP;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if(velocity.y < 0 && !isGrounded)
        {
            animator.SetBool("isFalling", true);
            animator.SetBool("isJumping", false);
        }

        else
        {
            animator.SetBool("isFalling", false);
        }
    }
}
