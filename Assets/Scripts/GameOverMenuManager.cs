using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenuManager : MonoBehaviour
{
    public GameObject[] gameOverObjects;

    // Start is called before the first frame update
    void Start()
    {
        gameOverObjects = GameObject.FindGameObjectsWithTag("showOnGameOver");
        hideGameOverObjects();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void restartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void launchMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void exitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void hideGameOverObjects()
    {
        foreach (GameObject gameObject in gameOverObjects)
            gameObject.SetActive(false);
    }

    public void showGameOverObjects()
    {
        foreach (GameObject gameObject in gameOverObjects)
            gameObject.SetActive(true);
    }
}