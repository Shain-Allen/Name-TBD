using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Vector3 preferedDirection = Vector3.zero;

    public Vector3 changeDirection = Vector3.zero;

    public Vector3 randomChangeDirection = Vector3.zero;

    public float speed = 10.0f;

    public float changeAmount = 0.1f;

    public float changeTime = 1.0f;

    public float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        
        if (timer >= changeTime)
        {
            timer = 0.0f;
            randomChangeDirection = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0.0f);
            preferedDirection += randomChangeDirection;
        }

        if (Input.GetKey(KeyCode.W))
        {
            changeDirection.y += changeAmount;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            changeDirection.y -= changeAmount;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            changeDirection.x += changeAmount;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            changeDirection.x -= changeAmount;
        }
        else
        {
            changeDirection -= changeDirection / 2.0f;
        }

        preferedDirection = new Vector3(Mathf.Clamp(preferedDirection.x, -1.0f, 1.0f), Mathf.Clamp(preferedDirection.y, -1.0f, 1.0f), 0.0f);

        gameObject.GetComponent<Rigidbody2D>().velocity = preferedDirection + changeDirection * speed * Time.deltaTime;
    }
}
