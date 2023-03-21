using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    public float score;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerController.gameOver)
        {
            if (playerController.isDoubleSpeed)
            {
                score += 2;
            }
            else
            {
                score++;
            }
            Debug.Log(score);
        }
    }
}
