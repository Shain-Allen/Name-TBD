using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject[] Levels;

    public GameObject Level;

    public int currentLevel = 0;

    public bool[] Collectibles;

    // Start is called before the first frame update
    void Start()
    {
        Collectibles = new bool[Levels.Length];

        LoadLevel(0);
    }

    public void SaveLevel()
    {
        Collectibles[currentLevel] = Levels[currentLevel].GetComponent<LevelController>().hasCollectible;
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
        Level.GetComponent<LevelController>().hasCollectible = Collectibles[levelNumber];
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
        SaveLevel();

        if (currentLevel < Levels.Length-1)
        {
            currentLevel++;
            LoadLevel(currentLevel);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
