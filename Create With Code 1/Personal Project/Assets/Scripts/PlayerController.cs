using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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
    private bool isInGroundSmash;
    private bool hasDoubleJumped;
    private bool hasAirDashed;
    private bool canMove;

    private int dashCount;
    private bool canDash;

    public List<GameObject> interactables = new List<GameObject>();
    public List<GameObject> targets = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityModifier;
        canMove = true;
        canDash = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Gets player input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);

        if (canMove)
        {
            // Basic Movement
            MovePlayer(horizontalInput, verticalInput, movement);

            // Advanced Movement
            HandlePlayerJump();
            HandleGroundSmash();
            HandlePlayerDash();
            HandlePlayerInteract();
            HandlePlayerAttack();
        }

        KeepPlayerInBounds();
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
            if (!isInGroundSmash)
            {
                isInGroundSmash = true;

                playerRb.useGravity = false;
                playerRb.velocity = Vector3.zero;

                StartCoroutine(HangtimeCooldown(.3f));
            }
        }

        if (isInGroundSmash && playerRb.useGravity)
        {
            playerRb.AddForce(Vector3.down * dashForce, ForceMode.Impulse);
        }
    }

    private void HandlePlayerDash()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (dashCount < 3 && canDash)
            {
                dashCount++;

                StartCoroutine(DashCooldown());

                if (!isOnGround && !hasAirDashed)
                {
                    playerRb.useGravity = false;

                    playerRb.velocity = Vector3.zero;

                    canDash = false;
                    hasAirDashed = true;
                    hasDoubleJumped = true;

                    StartCoroutine(DashLengthCooldown(2f));
                }
                else
                {
                    canDash = false;

                    StartCoroutine(DashLengthCooldown(.6f));
                }
            }
        }   
    }

    private void HandlePlayerInteract()
    {
        if (interactables.Count > 0 && interactables[0] == null)
        {
            interactables.RemoveAt(0);
        }

        if (interactables.Count == 0) { return; }

        if (Input.GetKeyDown(KeyCode.Slash))
        {
            interactables[0].GetComponent<IInteract>().onInteract();
        }
    }

    private void HandlePlayerAttack()
    {
        if (targets.Count > 0 && targets[0] == null)
        {
            targets.RemoveAt(0);
        }

        if (targets.Count == 0) { return; }

        if (Input.GetKeyDown(KeyCode.Slash))
        {
            targets[0].GetComponent<Health>().onTakeDamage(0);
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
            playerRb.useGravity = false;
            playerRb.velocity = Vector3.zero;

            StartCoroutine(HangtimeCooldown(.1f));

            playerRb.AddForce(Vector3.up * jumpForce * 0.7f, ForceMode.Impulse);
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

    // ============================= | Timers and Cooldowns | ============================= //

    IEnumerator DashLengthCooldown(float waitTime)
    {
        if (!playerRb.useGravity)
        {
            StartCoroutine(HangtimeCooldown(.3f));
        }

        playerRb.AddForce(playerAvatar.transform.forward * dashForce, ForceMode.VelocityChange);

        yield return new WaitForSeconds(waitTime);

        canDash = true;
    }

    IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(4f);

        dashCount--;
    }

    IEnumerator HangtimeCooldown(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        playerRb.useGravity = true;
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

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Collided with Enemy");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PickupItem>())
        {
            other.gameObject.GetComponent<PickupItem>().onPickup();
        }

        if (other.gameObject.GetComponent<Health>())
        {
            bool toAdd = true;

            for (int i = 0; i < targets.Count; i++)
            {
                if (other.gameObject == targets[i].gameObject)
                {
                    toAdd = false;
                }
            }

            if (toAdd)
            {
                targets.Add(other.gameObject);
            }
        }

        if (other.gameObject.GetComponent<Interactive>())
        {
            bool toAdd = true;

            for (int i = 0; i < interactables.Count; ++i)
            {
                if (other.gameObject == interactables[i].gameObject)
                {
                    toAdd = false;
                }
            }
            
            if (toAdd)
            {
                interactables.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        targets.Remove(other.gameObject);
        interactables.Remove(other.gameObject);
    }
}
