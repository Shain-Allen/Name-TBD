using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject[] Levels;

    public GameObject Level;

    public int currentLevel = 0;

[FMODUnity.EventRef]
    public string FinishNoise;

    FMOD.Studio.EventInstance PlayFinishNoise;

    void Awake()
    {
        PlayFinishNoise = FMODUnity.RuntimeManager.CreateInstance(FinishNoise);
    }
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
			//put sound here
            PlayFinishNoise.start();
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
