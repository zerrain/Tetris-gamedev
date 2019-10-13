using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject backgroundMusic;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(backgroundMusic);
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
