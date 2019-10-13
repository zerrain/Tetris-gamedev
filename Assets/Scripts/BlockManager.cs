using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BlockManager : MonoBehaviour
{
    private float prevTime; //Variable to store the last time in seconds the block was moved down
    private float timeBetweenDrops = 0.3f; //Interval block moves down in seconds
    public static int width = 10; 
    public static int height = 20;
    public Vector3 rotationPoint;
    private static Transform[,] tetrisBoard = new Transform[width, height]; //2D array for handling the tetris board
    public int score = 0;
    public Text scoreText;

    // Start is called before the first frame update, called upon the creation of a new block, if the new block placement is invalid, it is gameover
    void Start()
    {
        if (!isValidMove())
            gameOver();
        scoreText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        // Moves the block left if the left arrow key is pressed/held
        if (Input.GetKeyDown(KeyCode.LeftArrow) || (Time.frameCount % 20 == 0 && Input.GetKey(KeyCode.LeftArrow))) 
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!isValidMove())
                transform.position -= new Vector3(-1, 0, 0);
        }
        // Moves the block right if the right arrow key is pressed/held
        else if (Input.GetKeyDown(KeyCode.RightArrow) || (Time.frameCount % 20 == 0 && Input.GetKey(KeyCode.RightArrow)))
        {
            transform.position += new Vector3(1, 0, 0);
            if (!isValidMove())
                transform.position -= new Vector3(1, 0, 0);
        }
        // Rotates the block if the up arrow key is pressed
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
            if (!isValidMove())
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
        }
        // Moves the block down faster if the down arrow key is pressed/held
        if (Time.time - prevTime > (Input.GetKey(KeyCode.DownArrow) ? timeBetweenDrops / 8 : timeBetweenDrops))
        {
            transform.position += new Vector3(0, -1, 0);
            // If the block is under to move down anymore, it is added to the grid as a placed block and the function to spawn a new block is called
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

    // Function to check if the current move is a valid move
    bool isValidMove()
    {
        // Determines the x and y positions of each square in the current tetris block
        foreach (Transform child in transform)
        {
            int x = Mathf.RoundToInt(child.transform.position.x);
            int y = Mathf.RoundToInt(child.transform.position.y);

            // If the block would be moved to an invalid location outside of the tetris board
            if (x < 0 || x >= width || y < 0 || y >= height)
                return false;

            // If the block would be moved to a location where a block already exists
            if (tetrisBoard[x, y] != null)
                return false;
        }

        return true;
    }

    // Function responsible for adding the block to the tetris board by adding each square from the tetris block one by one 
    void addBlockToBoard()
    {
        foreach (Transform child in transform)
        {
            int x = Mathf.RoundToInt(child.transform.position.x);
            int y = Mathf.RoundToInt(child.transform.position.y);

            tetrisBoard[x, y] = child;
        }
    }

    // Checks for any complete lines
    void completeLinesCheck()
    {
        for (int i = height - 1; i >= 0; i--)
        {
            // If a complete line exists
            if (lineExists(i))
            {
                // Clear the line, move blocks down, add one to the score, add one to the score streak if the new featured mode
                clearLine(i);
                moveBlocksDown(i);
                score++;
                ScoreManager.score++;
                // If the current game mode is the new featured mode
                if (SceneManager.GetActiveScene().buildIndex == 2)
                {
                    ScoreStreakManager.scoreStreak++;
                    //If the scorestreak is achieved, clear the bottom most line after clearing the completed line and reset the scorestreak
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

    // Checks if a completed line exists
    bool lineExists(int i)
    {
        for (int j = 0; j < width; j++)
        {
            if (tetrisBoard[j, i] == null)
                return false;
        }

        return true;
    }

    // Clears the line
    void clearLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            Destroy(tetrisBoard[j, i].gameObject);
            tetrisBoard[j, i] = null;
        }
    }
    
    // Function responsible for moving the blocks down after a line is cleared
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

    // Function called when the game is over, displays game over menu and resets variables for a new game
    void gameOver()
    {
        ScoreManager.score = 0;
        ScoreStreakManager.scoreStreak = 0;
        GameObject.Find("Manager").GetComponent<GameOverMenuManager>().showGameOverObjects();
        Time.timeScale = 0;
    }
}
