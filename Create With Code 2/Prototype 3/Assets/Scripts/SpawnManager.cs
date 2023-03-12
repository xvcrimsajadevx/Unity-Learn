using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject obstaclePrefab;

    [SerializeField] private float startDelay = 2f;
    [SerializeField] private float repeatRate = 2f;

    private Vector3 spawnPos = new Vector3(25, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }   

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnObstacle()
    {
        Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
    }
}
