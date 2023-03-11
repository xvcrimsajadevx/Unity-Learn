using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public MeshRenderer Renderer;

    [SerializeField] private float translateRange = 5f;

    [SerializeField] private float maxBoxSize = 3f;
    [SerializeField] private float minBoxSize = 0.5f;

    private float colorTimer;
    private float timerRange;

    private Color previousColor;
    private Color currentColor;

    private Vector3 temp;
    
    void Start()
    {
        colorTimer = 0f;

        Material material = Renderer.material;

        float alpha = Random.Range(.3f, 1f);

        currentColor = new Color(Random.value, Random.value, Random.value, alpha);
        material.color = currentColor;

        transform.position = new Vector3(Random.Range(-4f, 4f), Random.Range(-4f, 4f), Random.Range(-4f, 4f));
        transform.localScale = Vector3.one * 1.3f;

        HandleTranslate();
    }

    void LateUpdate()
    {
        colorTimer -= Time.deltaTime;

        if (colorTimer <= 0)
        {
            float alpha = Random.Range(.3f, 1f);

            previousColor = currentColor;
            currentColor = new Color(Random.value, Random.value, Random.value, alpha);

            
            GenerateTimerRange();
            colorTimer = timerRange;
        }

        Material material = Renderer.material;
        material.color = Color.Lerp(previousColor, currentColor, Time.deltaTime / colorTimer);

        transform.Rotate(Random.Range(-60, -10) * Time.deltaTime, Random.Range(10, 40) * Time.deltaTime, Random.Range(-40, 40) * Time.deltaTime);

        HandleTranslate();
        HandleScale();

    }

    private void GenerateTimerRange()
    {
        timerRange = Random.Range(1f, 3f);
    }

    private void HandleScale()
    {
        // Randomly scales cube
        temp = transform.localScale;

        temp.x += Random.Range(-.3f, .3f) * Time.deltaTime * 10;
        temp.y += Random.Range(-.3f, .3f) * Time.deltaTime * 18;
        temp.z += Random.Range(-.3f, .3f) * Time.deltaTime * 20;

        // Keeps cube from getting too small or too large
        temp.x = Mathf.Clamp(temp.x, minBoxSize, maxBoxSize);
        temp.y = Mathf.Clamp(temp.y, minBoxSize, maxBoxSize);
        temp.z = Mathf.Clamp(temp.z, minBoxSize, maxBoxSize);

        // Applies new scale to transform
        transform.localScale = temp;
    }

    private void HandleTranslate()
    {
        // Randomly translates position of cube within a certain range
        transform.Translate(
            (Random.Range(-5f, 5f) * Time.deltaTime),
            (Random.Range(-5f, 5f) * Time.deltaTime),
            (Random.Range(-5f, 5f) * Time.deltaTime)
        );

        // Keeps cube within bounds on the screen
        if (transform.position.x > translateRange)
        {
            transform.position = new Vector3(translateRange, transform.position.y, transform.position.z);
        }
        if (transform.position.x < -translateRange)
        {
            transform.position = new Vector3(-translateRange, transform.position.y, transform.position.z);
        }

        if (transform.position.y > translateRange)
        {
            transform.position = new Vector3(transform.position.x, translateRange, transform.position.z);
        }
        if (transform.position.y < -translateRange)
        {
            transform.position = new Vector3(transform.position.x, -translateRange, transform.position.z);
        }

        if (transform.position.z > translateRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, translateRange);
        }
        if (transform.position.z < -translateRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -translateRange);
        }
    }
}
