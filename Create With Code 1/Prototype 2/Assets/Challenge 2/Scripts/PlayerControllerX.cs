using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    private float spawnTimer;
    private float spawnDelay = 0.8f;

    public GameObject dogPrefab;

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;

        // On spacebar press, send dog
        if (Input.GetKeyDown(KeyCode.Space) && spawnTimer >= spawnDelay)
        {
            Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);

            spawnTimer = 0;
        }
    }
}
