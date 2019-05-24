using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{

    FMODStudio.EventInstance PlayPickupSound;

    [FMODUnity.EventRef]
    public string PickupNoise;

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
