using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    public LevelManager LevelManager;

    public int LevelToLoad = -1;

    private bool load = false;

    private GameObject Fade;

    public Sprite[] anim;

    public float frameRate = 0.25f;

    private int frame = 0;

    private float timer = 0.0f;

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

        collider.gameObject.GetComponent<Movement>().enabled = false;
        collider.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Pickup") == null)
        {
            Debug.Log(frame);

            if(timer <= anim.Length) timer += Time.deltaTime;
            if (timer >= frameRate && frame < anim.Length - 1)
            {
                timer = 0.0f;
                gameObject.GetComponent<SpriteRenderer>().sprite = anim[++frame];
            }
        }

        if (!load) return;

        if (Fade == null) return;
        
        Fade.GetComponent<FadeController>().fade = true;

        if (Fade.GetComponent<FadeController>().t <= 1.0f) return;

        Fade.GetComponent<FadeController>().fade = false;

        Fade.GetComponent<FadeController>().t = 0.0f;

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
