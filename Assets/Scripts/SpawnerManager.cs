using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void spawnNewBlock()
    {
        Instantiate(tetrisBlocks[Random.Range(0, tetrisBlocks.Length)], transform.position, Quaternion.identity);
    }
}
