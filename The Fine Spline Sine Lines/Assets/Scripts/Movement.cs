using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject prefered;

    public GameObject change;

    [Header("Directions")]

    public Vector3 preferedDirection = Vector3.zero;

    public Vector3 changeDirection = Vector3.zero;

    public Vector3 randomChangeDirection = Vector3.zero;

    [Header("Ranges")]

    public Vector2 rangeRandChangeDir = Vector2.zero;

    public Vector2 rangeChangeDir = Vector2.zero;

    public Vector2 rangeprefDir = Vector2.zero;

    [Header("Speed")]

    public float speed = 10.0f;

    public float tSpeed = 0.1f;

    public float changeAmount = 0.1f;

    public float changeTime = 1.0f;
    

    [Header("Hidden")]
    private float timer = 0.0f;

    private float t = 0.0f;

    public string str = "";

    public bool isMoving = false;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        

        if (t >= 1.0f) t = 1.0f;
        else t += Time.deltaTime * tSpeed;
        
        if (timer >= changeTime)
        {
            timer = 0.0f;
            randomChangeDirection = new Vector3(Random.Range(rangeRandChangeDir.x, rangeRandChangeDir.y), Random.Range(rangeRandChangeDir.x, rangeRandChangeDir.y), 0.0f);
            preferedDirection += randomChangeDirection;
        }

        if (Input.GetKey(KeyCode.W))
        {
            changeDirection.y += changeAmount;
            t = 0.0f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            changeDirection.y -= changeAmount;
            t = 0.0f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            changeDirection.x += changeAmount;
            t = 0.0f;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            changeDirection.x -= changeAmount;
            t = 0.0f;
        }
        else
        {
            changeDirection = Vector3.Lerp(changeDirection, Vector3.zero, t);
        }

        if (Mathf.Approximately(changeDirection.x, Vector3.zero.x) && Mathf.Approximately(changeDirection.y, Vector3.zero.y))
        {
            change.SetActive(false);
            isMoving = false;
        }
        else
        {
            change.SetActive(true);

            isMoving = true;
        }

        preferedDirection = new Vector3(Mathf.Clamp(preferedDirection.x, rangeprefDir.x, rangeprefDir.y), Mathf.Clamp(preferedDirection.y, rangeprefDir.x, rangeprefDir.y), 0.0f);

        changeDirection = new Vector3(Mathf.Clamp(changeDirection.x, rangeChangeDir.x, rangeChangeDir.y), Mathf.Clamp(changeDirection.y, rangeChangeDir.x, rangeChangeDir.y), 0.0f);

        gameObject.GetComponent<Rigidbody2D>().velocity = (changeDirection + preferedDirection) * speed * Time.deltaTime;

        float preferedAngle = Mathf.Atan2(preferedDirection.y, preferedDirection.x) * Mathf.Rad2Deg;

        float changeAngle = Mathf.Atan2(changeDirection.y, changeDirection.x) * Mathf.Rad2Deg;

        prefered.GetComponent<Transform>().eulerAngles = new Vector3(0.0f, 0.0f, preferedAngle);

        change.GetComponent<Transform>().eulerAngles = new Vector3(0.0f, 0.0f, changeAngle);
    }
}
