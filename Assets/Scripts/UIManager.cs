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

    public void loadRecreatedLevel()
    {
        SceneManager.LoadScene("RecreatedGameScene");
    }

    public void loadNewFeatureLevel()
    {
        SceneManager.LoadScene("NewFeatureGameScene");
    }

    public void exitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
