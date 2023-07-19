using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController2048 : MonoBehaviour
{
    public static GameController2048 instance;
    public static int ticker;

    [SerializeField] private GameObject fillPrefab;
    [SerializeField] private Cell2048[] allCells;
    public static Action<string> slide;
    public int myScore;
    [SerializeField] Text scoreDisplay;

    int isGameOver;
    [SerializeField] GameObject gameOverPanel;

    //public Color[] fillColors;

    private void OnEnable()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        StartSpawnFill();
        StartSpawnFill();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            ticker = 0;
            isGameOver = 0;
            slide("w");
        }
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ticker = 0;
            isGameOver = 0;
            slide("a");
        }
        if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            ticker = 0;
            isGameOver = 0;
            slide("s");
        }
        if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            ticker = 0;
            isGameOver = 0;
            slide("d");
        }
    }
    public void SpawnFill()
    {
        float chance = UnityEngine.Random.Range(0f, 1f);
        bool isFull = true;
        for(int i = 0; i < allCells.Length; i++) 
        {
            if (allCells[i].fill == null)
            {
                isFull = false;
            } 
        }
        if (isFull == true) 
        {
            return;
        }

        /*
        List<int> available = new List<int>();
        for (int i = 0; i < allCells.Length; i++)
        {
            if (allCells[i].childCount == 0)
                available.Add(i);
        }
        */
        int whichSpawn = UnityEngine.Random.Range(0, allCells.Length);
        if (allCells[whichSpawn].transform.childCount != 0)
        {
            SpawnFill();
            return;
        }
        if (chance < .5f)
        {
            GameObject tempFill = Instantiate(fillPrefab, allCells[whichSpawn].transform);
            Debug.Log(2);
            Fill2048 tempFillComp = tempFill.GetComponent<Fill2048>();
            allCells[whichSpawn].GetComponent<Cell2048>().fill = tempFillComp;
            tempFillComp.FillValueUpdate(2);
        }
        else
        {
            GameObject tempFill = Instantiate(fillPrefab, allCells[whichSpawn].transform);
            Debug.Log(4);
            Fill2048 tempFillComp = tempFill.GetComponent<Fill2048>();
            allCells[whichSpawn].GetComponent<Cell2048>().fill = tempFillComp;
            tempFillComp.FillValueUpdate(4);

        }
    }
    public void StartSpawnFill()
    {
        bool isFull = true;
        for(int i = 0; i < allCells.Length; i++) 
        {
            if (allCells[i].fill == null)
            {
                isFull = false;
            } 
        }
        if (isFull == true) 
        {
            return;
        }
        int whichSpawn = UnityEngine.Random.Range(0, allCells.Length);
        if (allCells[whichSpawn].transform.childCount != 0)
        {
            SpawnFill();
            return;
        }
        GameObject tempFill = Instantiate(fillPrefab, allCells[whichSpawn].transform);
        Fill2048 tempFillComp = tempFill.GetComponent<Fill2048>();
        allCells[whichSpawn].GetComponent<Cell2048>().fill = tempFillComp;
        int[] fill_value = {2, 4};
        tempFillComp.FillValueUpdate(fill_value[UnityEngine.Random.Range(0, 2)]);
    }

    public void ScoreUpdate(int scoreIn)
    {
        myScore += scoreIn;
        scoreDisplay.text = myScore.ToString();
    }     

    public void GameOverCheck()
    {
        isGameOver++;
        if(isGameOver >= 16)
        {
            gameOverPanel.SetActive(true);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
