using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    private int score = 0;
    private int health = 100;
    private Text scoreDisplay;
    private Text healthDisplay;

    void Start()
    {
        scoreDisplay = GameObject.Find("ScoreCounter").GetComponent<Text>();
        healthDisplay = GameObject.Find("HealthCounter").GetComponent<Text>();
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        scoreDisplay.text = $"Score: {score}";
        healthDisplay.text = $"Health: {health}";
    }

    public void AddScore(int toAdd)
    {
        score += toAdd;
        UpdateDisplay();
    }

    public void AddHealth(int toAdd)
    {
        health += toAdd;
        UpdateDisplay();
    }
}
