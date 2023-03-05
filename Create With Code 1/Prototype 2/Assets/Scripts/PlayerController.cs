using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;

    [SerializeField] private float speed = 10f;
    [SerializeField] private float xRange = 10f;
    public float horizontalInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Gets player input
        horizontalInput = Input.GetAxis("Horizontal");

        // Moves player
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);

        // Keeps Player inbound
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.y);
        }
        else if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.y);
        }
    }
}
