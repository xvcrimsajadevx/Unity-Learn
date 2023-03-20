using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private ParticleSystem explosionParticle;
    [SerializeField] private ParticleSystem dirtParticle;

    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip crashSound;

    [SerializeField] private float jumpForce = 10.0f;
    [SerializeField] private float gravityModifier;

    private bool isOnGround = true;
    private bool hasDoubleJumped;
    public bool gameOver {  get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !gameOver && !hasDoubleJumped)
        {
            if (isOnGround)
            {
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

                isOnGround = false;
            }
            else if (!hasDoubleJumped)
            {
                hasDoubleJumped = true;

                playerRb.AddForce(Vector3.up * jumpForce * 0.7f, ForceMode.Impulse);
            }

            animator.SetTrigger("Jump_trig");
            audioSource.PlayOneShot(jumpSound, 0.4f);
        }

        if (!isOnGround || gameOver)
        {
            dirtParticle.Stop();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            hasDoubleJumped = false;
            dirtParticle.Play();
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;

            Debug.Log("gameOver");

            animator.SetInteger("DeathType_int", 1);
            animator.SetBool("Death_b", true);

            explosionParticle.Play();
            audioSource.PlayOneShot(crashSound, 1.0f);
        }
        
    }
}
