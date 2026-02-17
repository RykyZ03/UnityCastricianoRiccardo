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

    private Vector3 horizontalMove;
    // Flag per bloccare lo spam del salto
    private bool isJumping = false;

    void Start()
    {
        anim = this.GetComponent<Animator>();
        cam = Camera.main;
        controller = this.GetComponent<CharacterController>();
    }

    void Update()
    {
        InputMagnitude();

        isGrounded = controller.isGrounded;

        if (isGrounded)
        {
            // Quando tocca terra resetta il flag di salto
            if (isJumping)
                isJumping = false;

            verticalVel = -0.5f;

            if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
            {
                verticalVel = Mathf.Sqrt(JumpHeight * 2f * Gravity);
                isJumping = true;
            }
        }
        else
        {
            // Applica gravità progressiva quando il player è in aria
            verticalVel -= Gravity * Time.deltaTime;
        }

        // Unica chiamata a Move() che combina XZ e Y nello stesso frame
        Vector3 finalMove = horizontalMove + new Vector3(0, verticalVel, 0);
        controller.Move(finalMove * Time.deltaTime);

        // Reset del vettore orizzontale per evitare valori residui nel frame successivo
        horizontalMove = Vector3.zero;
    }

    void PlayerMoveAndRotation()
    {
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

        // Calcola direzioni forward e right della camera, ignorando l'asse Y
        Vector3 forward = cam.transform.forward;
        Vector3 right = cam.transform.right;

        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        // Direzione di movimento relativa alla camera
        desiredMoveDirection = forward * InputZ + right * InputX;

        if (!blockRotationPlayer)
        {
            // Ruota il player gradualmente verso la direzione di movimento
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(desiredMoveDirection),
                desiredRotationSpeed
            );

            // Velocità ridotta in aria tramite airControlMultiplier
            // Salva il vettore senza chiamare Move(), lo farà Update()
            float currentVelocity = isGrounded ? Velocity : Velocity * airControlMultiplier;
            horizontalMove = desiredMoveDirection * currentVelocity;
        }
    }

    void InputMagnitude()
    {
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

        Speed = new Vector2(InputX, InputZ).sqrMagnitude;

        if (isGrounded)
        {
            if (Speed > allowPlayerRotation)
            {
                anim.SetFloat("Blend", Speed, StartAnimTime, Time.deltaTime);
                PlayerMoveAndRotation();
            }
            else
            {
                anim.SetFloat("Blend", Speed, StopAnimTime, Time.deltaTime);
            }
        }
        else
        {
            // In aria: blocca animazione di corsa
            anim.SetFloat("Blend", 0, StopAnimTime, Time.deltaTime);
            if (Speed > allowPlayerRotation)
            {
                PlayerMoveAndRotation();
            }
        }
    }

    // Chiamato da PlayerDeath per azzerare la caduta al momento del reset
    public void ResetVerticalVelocity()
    {
        verticalVel = 0f;
        isJumping = false;
    }
}