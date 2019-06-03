using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetOnTouch : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string deathNoise;

    FMOD.Studio.EventInstance PlayDeathNoise;
    
    private bool load = false;

    private GameObject Fade;

    void Awake()
    {
        PlayDeathNoise = FMODUnity.RuntimeManager.CreateInstance(deathNoise);
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name != "Player" || load) return;

        PlayDeathNoise.start();

        load = true;
        Fade = GameObject.Find("Fade");
        Fade.GetComponent<FadeController>().t = 0.0f;

        collision.gameObject.GetComponent<Movement>().enabled = false;
        collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;

        if (gameObject.name.Contains("Enemy")) gameObject.GetComponent<EnemyController>().enabled = false;
    }

    void Update()
    {
        if (!load) return;

        if (Fade == null) return;

        Fade.GetComponent<FadeController>().fade = true;

        if (Fade.GetComponent<FadeController>().t <= 1.0f) return;

        Fade.GetComponent<FadeController>().fade = false;

        Fade.GetComponent<FadeController>().t = 0.0f;

        GameObject.Find("Camera").GetComponent<LevelManager>().Reloadlevel();
    }
}
