﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject[] Levels;

    public float[] CameraSizes;

    public GameObject Level;

    public int currentLevel = 0;

    // Start is called before the first frame update
    void Start()
    {
        LoadLevel(0);
    }

    public void DestroyLevel()
    {
        if (Level != null) Destroy(Level);
    }

    public void CreateLevel(int levelNumber, bool destroyPrevious = true)
    {
        if (destroyPrevious) DestroyLevel();

        Level = Instantiate(Levels[levelNumber]);

        CameraController cc = gameObject.GetComponent<CameraController>();

        cc.top = GameObject.Find("Top");
        cc.bottom = GameObject.Find("Bottom");
        cc.right = GameObject.Find("Right");
        cc.left = GameObject.Find("Left");

        cc.player = GameObject.Find("Player");

        currentLevel = levelNumber;
    }

    public void Reloadlevel()
    {
        CreateLevel(currentLevel);
    }

    public void LoadLevel(int levelNumber)
    {
        CreateLevel(levelNumber);
    }

    public void LoadNextLevel()
    {
        if (currentLevel < Levels.Length - 1)
        {
            currentLevel++;
            LoadLevel(currentLevel);
        }
        else
        {
            LoadLevel(0);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
