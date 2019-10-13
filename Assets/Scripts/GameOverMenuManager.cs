using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenuManager : MonoBehaviour
{
    public GameObject[] gameOverObjects; //Contains game over objects such as game over menu buttons and title text

    // Start is called before the first frame update
    void Start()
    {
        // Initializes game over objects
        gameOverObjects = GameObject.FindGameObjectsWithTag("showOnGameOver");
        hideGameOverObjects();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Loads the current scene again
    public void restartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Loads the main menu scene
    public void launchMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    // Exits the game
    public void exitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }

    // Hides all game over objects
    public void hideGameOverObjects()
    {
        foreach (GameObject gameObject in gameOverObjects)
            gameObject.SetActive(false);
    }

    // Displays all game over objects
    public void showGameOverObjects()
    {
        foreach (GameObject gameObject in gameOverObjects)
            gameObject.SetActive(true);
    }
}