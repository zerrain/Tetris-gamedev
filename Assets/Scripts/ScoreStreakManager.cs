using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Class responsible for handling the score streak of the player
public class ScoreStreakManager : MonoBehaviour
{
    public static int scoreStreak = 0;
    Text scoreStreakText;

    // Start is called before the first frame update
    void Start()
    {
        scoreStreakText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreStreakText.text = "Score Streak: " + scoreStreak;
    }
}
