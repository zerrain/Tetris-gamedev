using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class responsible for spawning new blocks at random
public class SpawnerManager : MonoBehaviour
{
    public GameObject[] tetrisBlocks;

    // Start is called before the first frame update
    void Start()
    {
        spawnNewBlock();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Randomly instantiates a new tetris block prefab from the set ones in the tetrisBlocks array
    public void spawnNewBlock()
    {
        Instantiate(tetrisBlocks[Random.Range(0, tetrisBlocks.Length)], transform.position, Quaternion.identity);
    }
}
