using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyPulse : MonoBehaviour
{
    public float speed;

    private float T = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if (T <= 6.28f) T += Time.deltaTime * speed;
        else T = 0.0f;

        gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Abs(Mathf.Sin(T)));
    }
}
