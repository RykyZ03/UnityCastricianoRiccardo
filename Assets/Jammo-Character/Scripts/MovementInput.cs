using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MovementInput : MonoBehaviour
{
    public float Velocity;
    [Space]
    public float InputX;
    public float InputZ;
    public Vector3 desiredMoveDirection;
    public bool blockRotationPlayer;
    public float desiredRotationSpeed = 0.1f;
    public Animator anim;
    public float Speed;
    public float allowPlayerRotation = 0.1f;
    public Camera cam;
    public CharacterController controller;
    public bool isGrounded;

    [Header("Animation Smoothing")]
    [Range(0, 1f)] public float HorizontalAnimSmoothTime = 0.2f;
    [Range(0, 1f)] public float VerticalAnimTime = 0.2f;
    [Range(0, 1f)] public float StartAnimTime = 0.3f;
    [Range(0, 1f)] public float StopAnimTime = 0.15f;

    [Header("Jump Settings")]
    public float JumpHeight = 5f;
    public float Gravity = 9.81f;
    public float verticalVel;

    [Header("Air Control")]
    [Range(0, 1f)] public float airControlMultiplier = 0.3f;

    private Vector3 moveVector;

    void Start()
    {
        anim = this.GetComponent<Animator>();
        cam = Camera.main;
        controller = this.GetComponent<CharacterController>();
    }

    void Update()
    {
        InputMagnitude();

        // Ground check
        isGrounded = controller.isGrounded;

        if (isGrounded)
        {
            verticalVel = -0.5f;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVel = Mathf.Sqrt(JumpHeight * 2f * Gravity);
            }
        }
        else
        {
            verticalVel -= Gravity * Time.deltaTime;
        }

        // Applica movimento verticale
        moveVector = new Vector3(0, verticalVel, 0);
        controller.Move(moveVector * Time.deltaTime);
    }

    void PlayerMoveAndRotation()
    {
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

        Vector3 forward = cam.transform.forward;
        Vector3 right = cam.transform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        desiredMoveDirection = forward * InputZ + right * InputX;

        if (!blockRotationPlayer)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);

            // Applica velocità ridotta se in aria
            float currentVelocity = isGrounded ? Velocity : Velocity * airControlMultiplier;
            controller.Move(desiredMoveDirection * Time.deltaTime * currentVelocity);
        }
    }

    void InputMagnitude()
    {
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

        Speed = new Vector2(InputX, InputZ).sqrMagnitude;

        if (isGrounded)
        {
            // A terra: animazione e movimento normali
            if (Speed > allowPlayerRotation)
            {
                anim.SetFloat("Blend", Speed, StartAnimTime, Time.deltaTime);
                PlayerMoveAndRotation();
            }
            else if (Speed < allowPlayerRotation)
            {
                anim.SetFloat("Blend", Speed, StopAnimTime, Time.deltaTime);
            }
        }
        else
        {
            // In aria: blocca animazione ma permetti movimento
            anim.SetFloat("Blend", 0, StopAnimTime, Time.deltaTime);

            // Permetti movimento in aria se c'è input
            if (Speed > allowPlayerRotation)
            {
                PlayerMoveAndRotation();
            }
        }
    }

    public void ResetVerticalVelocity()
    {
        verticalVel = 0f;
    }
}