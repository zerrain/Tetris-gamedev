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
        pauseObjects = GameObject.FindGameObjectsWithTag("showOnPause");
        hidePauseObjects();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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

    public void restartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void launchMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void hidePauseObjects()
    {
        foreach (GameObject gameObject in pauseObjects)
            gameObject.SetActive(false);
    }

    public void showPauseObjects()
    {
        foreach (GameObject gameObject in pauseObjects)
            gameObject.SetActive(true);
    }
}
