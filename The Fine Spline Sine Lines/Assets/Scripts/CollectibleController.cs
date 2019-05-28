using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleController : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string pickupNoise;

    FMOD.Studio.EventInstance PlayPickupSound;

    public LevelManager levelManager;


    void Awake()
    {
        PlayPickupSound = FMODUnity.RuntimeManager.CreateInstance(pickupNoise);
    }


    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name != "Player") return;

        PlayPickupSound.start();
        Debug.Log("heck");

        levelManager = GameObject.Find("Camera").GetComponent<LevelManager>();
        
        levelManager.Collectibles[levelManager.currentLevel] = true;

        Destroy(gameObject);
    }
}
