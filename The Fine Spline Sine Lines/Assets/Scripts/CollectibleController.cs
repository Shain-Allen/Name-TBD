using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleController : MonoBehaviour
{
    public LevelController levelController;

    // Start is called before the first frame update
    void Start()
    {
        levelController = GameObject.FindWithTag("Level").GetComponent<LevelController>();
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name != "Dot") return;

        levelController.hasCollectible = true;
        Destroy(gameObject);
    }
}
