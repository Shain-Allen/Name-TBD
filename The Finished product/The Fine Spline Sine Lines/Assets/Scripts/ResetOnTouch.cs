using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetOnTouch : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string deathNoise;

    FMOD.Studio.EventInstance PlayDeathNoise;

    void Awake()
    {
        PlayDeathNoise = FMODUnity.RuntimeManager.CreateInstance(deathNoise);
    }

    // Update is called once per frame
    void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.gameObject.name != "Player") return;

        PlayDeathNoise.start();

        GameObject.Find("Camera").GetComponent<LevelManager>().Reloadlevel();
	}
}
