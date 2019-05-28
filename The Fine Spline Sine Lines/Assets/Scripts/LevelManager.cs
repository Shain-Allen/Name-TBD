using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject[] Levels;

    public GameObject Level;

    public int currentLevel = 0;

    public bool[] Collectibles;

    public float[] Times;

    // Start is called before the first frame update
    void Start()
    {
        Collectibles = new bool[Levels.Length];
        Collectibles = new bool[Levels.Length];

        Times = new float[Levels.Length];

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
