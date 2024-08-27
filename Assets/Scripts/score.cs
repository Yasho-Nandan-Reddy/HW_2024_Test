using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Assuming you want to display the score on the UI

public class score : MonoBehaviour
{
    public Text scoreText; // UI Text component to display the score

    // Method to update the score
    public void UpdateScore(int newScore)
    {
        scoreText.text = "Score: " + newScore.ToString();
        Debug.Log("Score: " + newScore);
    }

    void Start()
    {
        // Initialize the score display
        UpdateScore(0);
    }

    void Update()
    {

    }
}
