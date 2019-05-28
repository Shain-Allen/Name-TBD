using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    public LevelManager LevelManager;

    // Start is called before the first frame update
    void Start()
    {
        LevelManager = GameObject.Find("Camera").GetComponent<LevelManager>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name != "Player") return;
        LevelManager.LoadNextLevel();
        Destroy(gameObject);
    }
}
