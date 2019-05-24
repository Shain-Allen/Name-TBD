using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string PickupNoise;

    FMODStudio.EventInstance PlayPickupSound;


	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D (Collider2D collider2D)
    {
        if (collider2D.name != "Player") return;

        PlayPickupSound.start();
        Destroy(gameObject);
	}

    void awake()
    {
        PlayPickupSound = FMODUnity.RuntimeManager.CreateInstance(PickupNoise);
    }

}
