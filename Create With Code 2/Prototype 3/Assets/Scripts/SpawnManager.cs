using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] ObstaclePrefabs;

    [SerializeField] private float startDelay = 2f;
    [SerializeField] private float repeatRate = 2f;

    private Vector3 spawnPos = new Vector3(25, 0, 0);
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }   

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnObstacle()
    {
        int obstacleIndex = Random.Range(0, ObstaclePrefabs.Length);

        if (!playerController.gameOver)
        {
            Instantiate(ObstaclePrefabs[obstacleIndex], spawnPos, ObstaclePrefabs[obstacleIndex].transform.rotation);
        }
    }
}
