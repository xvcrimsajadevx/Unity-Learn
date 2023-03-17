using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    // References to player components
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private CapsuleCollider playerCollider;

    // Gameplay variables
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float jumpForce = 7.0f;
    [SerializeField] private float dashForce = 12.0f;
    [SerializeField] private float gravityModifier = 1.5f;

    [SerializeField] private float areaBound = 24.0f;

    //Gameplay booleans
    private bool isOnGround;
    private bool hasDoubleJumped;
    private bool hasAirDashed;

    private float dashCounter;
    private float dashHangTime = 0.3f;
    private float dashCooldown = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        // Gets player input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Counter ticks
        dashCounter += Time.deltaTime;

        // Basic Movement
        MovePlayer(horizontalInput, verticalInput);

        // Advanced Movement
        HandlePlayerJump();
        HandlePlayerDash();

        KeepPlayerInBounds();
    }

    private void HandlePlayerDash()
    {
        if (Input.GetKeyDown(KeyCode.B) && dashCounter > dashCooldown)
        {
            dashCounter = 0;

            if (!isOnGround && !hasAirDashed)
            {
                playerRb.useGravity = false;

                playerRb.velocity = Vector3.zero;

                hasAirDashed = true;
            }

            playerRb.AddForce(Vector3.forward * dashForce, ForceMode.VelocityChange);
        }
        else if (dashCounter > dashHangTime)
        {
            playerRb.useGravity = true;
        }
    }

    private void MovePlayer(float horizontalInput, float verticalInput) 
    {
        // Moves player according to axis Input
        transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * speed);
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
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
            playerRb.AddForce(Vector3.up * jumpForce * 0.7f, ForceMode.Impulse);
            hasDoubleJumped = true;
        }
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Registers whether player is on ground and resets hasDoubleJumped bool
            isOnGround = true;
            hasDoubleJumped = false;
            hasAirDashed = false;
        }
    }
}
