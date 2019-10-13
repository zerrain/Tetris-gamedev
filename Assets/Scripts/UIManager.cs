using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject backgroundMusic;
    static bool firstRun = true;

    // Start is called before the first frame update
    void Start()
    {
        // Plays the background music if it is the initial run of this scene and sets it to be persistent between scenes
        if (firstRun)
        {
            backgroundMusic.GetComponent<AudioSource>().Play();
            DontDestroyOnLoad(backgroundMusic);
            firstRun = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Loads the originial recreated tetris scene
    public void loadRecreatedLevel()
    {
        SceneManager.LoadScene("RecreatedGameScene");
    }

    // Loads the new features design iteration tetris scene
    public void loadNewFeatureLevel()
    {
        SceneManager.LoadScene("NewFeatureGameScene");
    }

    // Exits the game
    public void exitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
