using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleController : MonoBehaviour
{
    public LevelManager levelManager;

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name != "Player") return;

        levelManager = GameObject.Find("Camera").GetComponent<LevelManager>();
        
        levelManager.Collectibles[levelManager.currentLevel] = true;

        Destroy(gameObject);
    }
}
