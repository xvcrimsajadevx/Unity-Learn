using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    // References to player components
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private CapsuleCollider playerCollider;
    [SerializeField] private GameObject cameraFocus;
    [SerializeField] private GameObject playerAvatar;

    // Gameplay variables
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float turnSpeed = 20f;
    [SerializeField] private float jumpForce = 7.0f;
    [SerializeField] private float dashForce = 12.0f;
    [SerializeField] private float gravityModifier = 1.5f;

    [SerializeField] private float areaBound = 24.0f;

    //Gameplay booleans
    private bool isOnGround;
    private bool isDashing;
    private bool isInGroundSmash;
    private bool hasDoubleJumped;
    private bool hasAirDashed;
    private bool canMove;

    private float dashCounter;
    private float dashHangTime = 0.3f;
    private float dashCooldown = 0.6f;

    private float smashCounter;
    private float smashHangTime = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityModifier;
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Gets player input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);

        // Counter ticks
        dashCounter += Time.deltaTime;
        smashCounter += Time.deltaTime;

        if (canMove)
        {
            // Basic Movement
            MovePlayer(horizontalInput, verticalInput, movement);

            // Advanced Movement
            HandlePlayerJump();
            HandleGroundSmash();
            HandlePlayerDash();
        }

        KeepPlayerInBounds();
        FixGravity();
    }

    // ================================== | Gameplay Methods | ================================== //

    private void MovePlayer(float horizontalInput, float verticalInput, Vector3 movement)
    {
        // Moves player according to axis Input
        transform.Translate(cameraFocus.transform.right * horizontalInput * Time.deltaTime * speed);
        transform.Translate(cameraFocus.transform.forward * verticalInput * Time.deltaTime * speed);

        if (movement.magnitude > 0)
        {
            Quaternion cameraForward = Quaternion.LookRotation(cameraFocus.transform.forward);
            Quaternion moveRotation = Quaternion.LookRotation(movement);

            playerAvatar.transform.rotation = Quaternion.Slerp(playerAvatar.transform.rotation, cameraForward * moveRotation, Time.deltaTime * turnSpeed);
        }
    }

    private void HandleGroundSmash()
    {
        if (Input.GetKeyDown(KeyCode.N) && !isOnGround)
        {
            smashCounter = 0;
            isInGroundSmash = true;

            if (smashCounter < smashHangTime && isInGroundSmash)
            {
                playerRb.useGravity = false;
                playerRb.velocity = Vector3.zero;
            }
        }
        else if (smashCounter > smashHangTime && isInGroundSmash)
        {
            playerRb.AddForce(Vector3.down * dashForce, ForceMode.Impulse);  
        }
    }

    private void HandlePlayerDash()
    {
        if (Input.GetKeyDown(KeyCode.B) && dashCounter > dashCooldown)
        {
            dashCounter = 0;
            isDashing = true;

            if (!isOnGround && !hasAirDashed)
            {
                playerRb.useGravity = false;

                playerRb.velocity = Vector3.zero;

                hasAirDashed = true;
                hasDoubleJumped = true;
            }

            playerRb.AddForce(playerAvatar.transform.forward * dashForce, ForceMode.VelocityChange);
        }
        else if (dashCounter > dashHangTime)
        {
            isDashing = false;
        }
    }

    private void HandlePlayerJump() 
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            // Player jumps from ground when space bar pressed
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !isOnGround && !hasDoubleJumped)
        {
            // Adds second jump in air if player hasn't already double-jumped
            playerRb.velocity = Vector3.zero;

            playerRb.AddForce(Vector3.up * jumpForce * 0.8f, ForceMode.Impulse);
            hasDoubleJumped = true;
        }
    }

    // ============================= | Restrictions and Controls | ============================= //

    public void ControlFreezePlayer()
    {
        canMove = !canMove;
    }

    private void KeepPlayerInBounds()
    {
        // Constraining character to ground plane (Temporary)
        if (transform.position.x > areaBound) {
            transform.position = new Vector3(areaBound, transform.position.y, transform.position.z); }

        if (transform.position.x < -areaBound) {
            transform.position = new Vector3(-areaBound, transform.position.y, transform.position.z); }

        if (transform.position.z > areaBound) {
            transform.position = new Vector3(transform.position.x, transform.position.y, areaBound); }

        if (transform.position.z < -areaBound) {
            transform.position = new Vector3(transform.position.x, transform.position.y, -areaBound); }
    }

    private void FixGravity()
    {
        if (!isDashing && !isInGroundSmash)
        {
            if (!playerRb.useGravity)
            {
                playerRb.useGravity = true;
            }
        }
    }

    // ============================= | Collisions and Feedback | ============================= //

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Registers whether player is on ground and resets hasDoubleJumped bool
            isOnGround = true;
            hasDoubleJumped = false;
            hasAirDashed = false;

            if (isInGroundSmash)
            {
                transform.position = new Vector3(transform.position.x, collision.transform.position.y + 1, transform.position.z);

                if (collision.gameObject.name == "Ground")
                {
                    float impactForce = 30f;

                    playerRb.velocity = Vector3.zero;
                    transform.position = new Vector3(transform.position.x, collision.transform.position.y + 1, transform.position.z);

                    isInGroundSmash = false;

                    if (!isInGroundSmash && isOnGround)
                    {
                        playerRb.AddForce(Vector3.up * impactForce, ForceMode.Impulse);
                    }
                } 
            }
        }

    }

    

    
}
