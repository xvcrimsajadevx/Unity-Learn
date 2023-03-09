using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == "Food" && other.gameObject.tag == "Animal")
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
        else if (gameObject.tag == "Animal" && other.gameObject.tag == "Player")
        {
            Destroy(other.gameObject);

            Debug.Log("Game Over!");
        }
        else
        {
            return;
        }
        
    }
}
