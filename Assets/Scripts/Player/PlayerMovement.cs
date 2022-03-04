using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    #region Fields
    private float xInput;
    private float zInput;
    private Vector3 movement;

    [Header("CharacterController")]
    [SerializeField]
    private CharacterController controller;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float gravity;
    [SerializeField]
    private float jumpHeight;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private LayerMask groundMask;
    private float groundDistance;

    private Vector3 velocity;
    private bool isGrounded;
    #endregion

    void Start() {
        //Default speed
        speed = 12;
        //Default Gravity
        gravity = -9.81f * 2;
        //Default JumpHeight
        jumpHeight = 3f;
        //Default ground distance 
        groundDistance = 0.4f;
    }

    void Update()  {
        //Check ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }

        //Get Input
        xInput = Input.GetAxis("Horizontal");
        zInput = Input.GetAxis("Vertical");
        //Set movement
        movement = transform.right * xInput + transform.forward * zInput;
        controller.Move(movement * speed * Time.deltaTime);
        //Jump
        if (Input.GetButtonDown("Jump") && isGrounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        //Hanlde gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime); //Free fall physics g*^2
    }
}
