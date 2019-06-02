using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    public float fadeSpeed = 2.0f;

    public float t = 0.0f;

    public bool fade = false;

    public void FadeOut()
    {
        t += Time.deltaTime * fadeSpeed;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(0.0f, 0.0f, 0.0f, Mathf.Lerp(0.0f, 1.0f, t));
    }

    public void FadeIn()
    {
        t += Time.deltaTime * fadeSpeed;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(0.0f, 0.0f, 0.0f, Mathf.Lerp(1.0f, 0.0f, t));
    }

    // Update is called once per frame
    void Start()
    {
    }

    void Update()
    {
        if (fade)
        {
            FadeOut();
        }
        else
        {
            FadeIn();
        }
    }
}
