using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject[] pauseObjects;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;

        //Initializes pause menu objects
        pauseObjects = GameObject.FindGameObjectsWithTag("showOnPause");
        hidePauseObjects();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Pauses the game if it is resumed, otherwise resumes the game if it is paused
    public void pauseGame()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            showPauseObjects();
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            hidePauseObjects();
        }
    }

    // Restarts the current scene
    public void restartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Starts the main menu scene
    public void launchMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    // Hides all pause menu objects
    public void hidePauseObjects()
    {
        foreach (GameObject gameObject in pauseObjects)
            gameObject.SetActive(false);
    }

    // Displays all pause menu objects
    public void showPauseObjects()
    {
        foreach (GameObject gameObject in pauseObjects)
            gameObject.SetActive(true);
    }
}
