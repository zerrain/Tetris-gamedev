using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BlockManager : MonoBehaviour
{
    private float prevTime;
    private float timeBetweenDrops = 0.3f;
    public static int width = 10;
    public static int height = 20;
    public Vector3 rotationPoint;
    private static Transform[,] tetrisBoard = new Transform[width, height];
    public int score = 0;
    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        if (!isValidMove())
            gameOver();
        scoreText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || (Time.frameCount % 20 == 0 && Input.GetKey(KeyCode.LeftArrow))) 
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!isValidMove())
                transform.position -= new Vector3(-1, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || (Time.frameCount % 20 == 0 && Input.GetKey(KeyCode.RightArrow)))
        {
            transform.position += new Vector3(1, 0, 0);
            if (!isValidMove())
                transform.position -= new Vector3(1, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
            if (!isValidMove())
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
        }

        if (Time.time - prevTime > (Input.GetKey(KeyCode.DownArrow) ? timeBetweenDrops / 8 : timeBetweenDrops))
        {
            transform.position += new Vector3(0, -1, 0);
            if (!isValidMove())
            {
                transform.position -= new Vector3(0, -1, 0);
                addBlockToBoard();
                completeLinesCheck();
                this.enabled = false;
                FindObjectOfType<SpawnerManager>().spawnNewBlock();
            }
            prevTime = Time.time;
        }
    }

    bool isValidMove()
    {
        foreach (Transform child in transform)
        {
            int x = Mathf.RoundToInt(child.transform.position.x);
            int y = Mathf.RoundToInt(child.transform.position.y);

            if (x < 0 || x >= width || y < 0 || y >= height)
                return false;

            if (tetrisBoard[x, y] != null)
                return false;
        }

        return true;
    }

    void addBlockToBoard()
    {
        foreach (Transform child in transform)
        {
            int x = Mathf.RoundToInt(child.transform.position.x);
            int y = Mathf.RoundToInt(child.transform.position.y);

            tetrisBoard[x, y] = child;
        }
    }

    void completeLinesCheck()
    {
        for (int i = height - 1; i >= 0; i--)
        {
            if (lineExists(i))
            {
                clearLine(i);
                moveBlocksDown(i);
                score++;
                ScoreManager.score++;
                if (SceneManager.GetActiveScene().buildIndex == 2)
                {
                    ScoreStreakManager.scoreStreak++;
                    if (ScoreStreakManager.scoreStreak == 5)
                    {
                        ScoreStreakManager.scoreStreak = 0;
                        clearLine(0);
                        moveBlocksDown(0);
                    }
                }
            }
        }
    }

    bool lineExists(int i)
    {
        for (int j = 0; j < width; j++)
        {
            if (tetrisBoard[j, i] == null)
                return false;
        }

        return true;
    }

    void clearLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            Destroy(tetrisBoard[j, i].gameObject);
            tetrisBoard[j, i] = null;
        }
    }

    void moveBlocksDown(int i)
    {
        for (int j = i; j < height; j++)
        {
            for (int k = 0; k < width; k++)
            {
                if (tetrisBoard[k, j] != null)
                {
                    tetrisBoard[k, j - 1] = tetrisBoard[k, j];
                    tetrisBoard[k, j] = null;
                    tetrisBoard[k, j - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }
    }

    void gameOver()
    {
        ScoreManager.score = 0;
        ScoreStreakManager.scoreStreak = 0;
        GameObject.Find("Manager").GetComponent<GameOverMenuManager>().showGameOverObjects();
        Time.timeScale = 0;
    }
}
