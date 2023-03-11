using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Score: " + score);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ManageScore(int points)
    {
        score += points;

        Debug.Log("Score: " + score);
    }
}
