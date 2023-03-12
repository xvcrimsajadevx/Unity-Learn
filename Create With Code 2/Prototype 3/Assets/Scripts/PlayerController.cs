using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody playerRb;

    [SerializeField] private float jumpForce = 10.0f;
    [SerializeField] private float gravityModifier;

    private bool isOnGround = true;
    public bool gameOver {  get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            isOnGround = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;

            Debug.Log("gameOver");
        }
        
    }
}
