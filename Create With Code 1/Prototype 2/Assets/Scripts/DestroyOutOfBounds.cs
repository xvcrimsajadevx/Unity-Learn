using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    [SerializeField] private float zBound = 15.0f;
    [SerializeField] private float xBound = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z > zBound ||
            transform.position.x > xBound ||
            transform.position.x < -xBound ||
            transform.position.z < -zBound)
        {
            Destroy(gameObject);
        }
    }
}
