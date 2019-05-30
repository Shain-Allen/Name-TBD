using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string pickupNoise;

    FMOD.Studio.EventInstance PlayPickupSound;

    public LevelManager levelManager;

    public Vector2 amount = Vector2.zero;

    void Awake()
    {
        PlayPickupSound = FMODUnity.RuntimeManager.CreateInstance(pickupNoise);
    }

    void OnTriggerEnter2D (Collider2D collider2D)
    {
        if (collider2D.name != "Player") return;

        PlayPickupSound.start();
        //Debug.Log("heck");

        collider2D.GetComponent<Movement>().rangeChangeDir -= amount;

        Destroy(gameObject);
	}
}
