using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    public LevelManager LevelManager;

    public int LevelToLoad = -1;

    private bool load = false;

    private GameObject Fade;

    // Start is called before the first frame update
    void Start()
    {
        LevelManager = GameObject.Find("Camera").GetComponent<LevelManager>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name != "Player" || load) return;

        if (GameObject.FindGameObjectWithTag("Pickup") != null)
        {
            LevelManager.Reloadlevel();
            return;
        }

        load = true;
        Fade = GameObject.Find("Fade");
        Fade.GetComponent<FadeController>().t = 0.0f;
    }

    void Update()
    {
        if (!load) return;

        if (Fade == null) return;
        
        Fade.GetComponent<FadeController>().fade = true;

        if (Fade.GetComponent<FadeController>().t <= 1.0f) return;

        if (LevelToLoad == -1)
        {
            LevelManager.LoadNextLevel();
        }
        else
        {
            LevelManager.LoadLevel(LevelToLoad);
        }
    }
}
