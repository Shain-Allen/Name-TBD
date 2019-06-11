using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 2.0f;

    public Vector3 positionStart = Vector3.zero;

    public Vector3 positionEnd = Vector3.zero;

    public float waitTimeBetween = 0.1f;

    public float t = 0.0f;

    public bool forward = true;


    private void Start()
    {
        positionStart = transform.position + positionStart;

        positionEnd = transform.position + positionEnd;
    }
    // Update is called once per frame
    void Update()
    {
        Transform pos = gameObject.GetComponent<Transform>();

        t += Time.deltaTime * speed;

        if (pos.position.x == positionEnd.x && pos.position.y == positionEnd.y && t >= 1.0f)
        {
            forward = false;
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            t = 0.0f;
        }
        else if (pos.position.x == positionStart.x && pos.position.y == positionStart.y && t >= 1.0f)
        {
            forward = true;
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            t = 0.0f;
        }

        if (forward)
        {
            pos.position = Vector3.Lerp(positionStart, positionEnd, t);
        }
        else
        {
            pos.position = Vector3.Lerp(positionEnd, positionStart, t);
        }
    }
}
