using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Collider playerCollider;

    [SerializeField] private float speed = 10f;
    [SerializeField] private float xRange = 10f;
    [SerializeField] private float zRange = 10f;

    private float horizontalInput;
    private float verticalInput;

    private float pizzaTimer;

    private int lives = 3;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Lives Remaining: " + lives);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        pizzaTimer += Time.deltaTime;
        
        MovePlayer();
        KeepInbounds();
        
        LaunchPizza();
    }

    private void MovePlayer()
    {
        // Gets player input
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // Moves player
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
        transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * speed);
    }

    private void KeepInbounds()
    {
        // Keeps Player inbound
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }

        if (transform.position.z < -zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zRange);
        }
        else if (transform.position.z > zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zRange);
        }
    }

    private void LaunchPizza()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (pizzaTimer >= 0.1f)
            {
                // Launch projectile from the player
                Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation, gameObject.transform);

                pizzaTimer = 0f;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Animal")
        {
            lives--;

            if (lives > 0)
            {
                Debug.Log("Lives Remaining: " + lives);
            }
            else
            {
                Destroy(gameObject);
                Debug.Log("Game Over!");
            }
        }
    }
}
