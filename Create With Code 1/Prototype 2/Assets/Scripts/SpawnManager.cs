using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] AnimalPrefabs;
    [SerializeField] private SpawnManager spawnManager;
    [SerializeField] private ScoreManager scoreManager;

    [SerializeField] private float spawnRangeX = 15f;
    [SerializeField] private float spawnRangeZ = 19f;
    [SerializeField] private float spawnPosZ = 15f;
    [SerializeField] private float spawnPosX = 19f;

    [SerializeField] private float startDelay = 2f;
    [SerializeField] private float minSpawnLength = 0.5f;
    [SerializeField] private float maxSpawnLength = 2f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(SpawnRandomAnimal), startDelay, Random.Range(minSpawnLength, maxSpawnLength));
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SpawnRandomAnimal()
    {
        // Randomly generates animal index
        int animalIndex = Random.Range(0, AnimalPrefabs.Length);
        GameObject animal = AnimalPrefabs[animalIndex];

        // Randomly generates spawn position
        int spawnLocation = Random.Range(0, 3);
        Vector3 spawnPos;
        Quaternion newAnimalRotation;

        if (spawnLocation == 2)
        {
            spawnPos = spawnLeft();
            newAnimalRotation = Quaternion.LookRotation(Vector3.right, Vector3.up);
        }
        else if (spawnLocation == 1)
        {
            spawnPos = spawnRight();
            newAnimalRotation = Quaternion.LookRotation(Vector3.left, Vector3.up);
        }
        else
        {
            spawnPos = spawnTop();
            newAnimalRotation = Quaternion.LookRotation(Vector3.back, Vector3.up);
        }

        Instantiate(animal, spawnPos, newAnimalRotation, spawnManager.transform);
    }

    private Vector3 spawnLeft()
    {
        return new Vector3(-spawnPosX, 0, Random.Range(-spawnRangeZ, spawnRangeZ));
    }

    private Vector3 spawnRight()
    {
        return new Vector3(spawnPosX, 0, Random.Range(-spawnRangeZ, spawnRangeZ));
    }

    private Vector3 spawnTop()
    {
        return new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnPosZ);
    }
}
