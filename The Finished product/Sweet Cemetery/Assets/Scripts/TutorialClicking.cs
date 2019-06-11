using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialClicking : MonoBehaviour
{
    public Sprite MouseMoveSprite;

    public Sprite MouseClickSprite;

    public float moveSpeed = 2.0f;

    public float tMax = 1.0f;

    public float divTVal = 4.0f;

    private float t = 0.0f;

    public bool circleOrClick = false;

    // Start is called before the first frame update
    void Start()
    {
        if (circleOrClick)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = MouseMoveSprite;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = MouseClickSprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (circleOrClick)
        {
            transform.position = new Vector3(transform.parent.position.x + (Mathf.Cos(t)/2.0f), transform.parent.position.y + (Mathf.Sin(t)/2.0f), transform.parent.position.z + -0.1f);
            t += Time.deltaTime * moveSpeed;
            if (t >= tMax) t = 0.0f;
        }
        else
        {
            t += Time.deltaTime * moveSpeed;
            if (t <= tMax / divTVal)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = MouseClickSprite;
            }
            else if (t >= tMax / divTVal && t <= tMax)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = MouseMoveSprite;
            }
            else if (t >= tMax)
            {
                t = 0.0f;
            }
        }
    }
}
