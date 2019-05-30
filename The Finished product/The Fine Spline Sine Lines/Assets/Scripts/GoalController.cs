using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    public LevelManager LevelManager;

    public int LevelToLoad = -1;

    // Start is called before the first frame update
    void Start()
    {
        LevelManager = GameObject.Find("Camera").GetComponent<LevelManager>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name != "Player") return;

        if (GameObject.FindGameObjectWithTag("Pickup") != null)
        {
            LevelManager.Reloadlevel();
            return;
        }

        if (LevelToLoad == -1)
        {
            LevelManager.LoadNextLevel();
        }
        else
        {
            LevelManager.LoadLevel(LevelToLoad);
        }

        Destroy(gameObject);
    }
}
