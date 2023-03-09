using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] animalPrefabs;

    [SerializeField] private float spawnRangeX = 15;
    [SerializeField] private float spawnRangeZ = 19;
    [SerializeField] private float spawnPosZ = 15;
    [SerializeField] private float spawnPosX = 19;

    [SerializeField] private float startDelay = 2f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(SpawnRandomAnimal), startDelay, Random.Range(0.5f, 3f));
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SpawnRandomAnimal()
    {
        // Randomly generates animal index
        int animalIndex = Random.Range(0, animalPrefabs.Length);

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

        Instantiate(animalPrefabs[animalIndex], spawnPos, newAnimalRotation);
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
