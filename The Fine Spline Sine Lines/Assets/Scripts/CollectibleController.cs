using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleController : MonoBehaviour
{
    public LevelController levelController;

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name != "Dot") return;

        levelController = GameObject.FindWithTag("Level").GetComponent<LevelController>();

        levelController.hasCollectible = true;

        Debug.Log("Got");
        Destroy(gameObject);
    }
}
