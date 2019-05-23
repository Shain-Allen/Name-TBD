using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotController : MonoBehaviour
{
    [Header("Bools")]

    public bool useMouse = false;

    [Header("Stats")]

    public GameObject levelController;

    public GameObject arrow;

    public Vector3 respawnPos = Vector3.zero;

    public float moveSpeed = 10.0f;

    public int yIStart = 0;

    public int xIStart = 0;

    public int yIndex = 5;

    public int xIndex = 5;
    
    void MoveDot(float dt)
    {
        LevelController lc = levelController.GetComponent<LevelController>();

        if (Input.GetKeyDown(KeyCode.W) && yIndex < lc.yWave.Length)
        {
            ++yIndex;
        }
        else if (Input.GetKeyDown(KeyCode.W) && yIndex >= lc.yWave.Length)
        {
            yIndex = 1;
        }

        if (Input.GetKeyDown(KeyCode.S) && yIndex > 1)
        {
            --yIndex;
        }
        else if (Input.GetKeyDown(KeyCode.S) && yIndex <= 1)
        {
            yIndex = lc.yWave.Length;
        }

        if (Input.GetKeyDown(KeyCode.D) && xIndex < lc.xWave.Length)
        {
            ++xIndex;
        }
        if (Input.GetKeyDown(KeyCode.D) && xIndex >= lc.xWave.Length)
        {
            xIndex = 0;
        }

        if (Input.GetKeyDown(KeyCode.A) && xIndex > 1)
        {
            --xIndex;
        }
        else if (Input.GetKeyDown(KeyCode.A) && xIndex <= 1)
        {
            xIndex = lc.xWave.Length;
        }

        if (xIndex > 0 && xIndex < lc.xWave.Length)
        {
            lc.xDisplayLine.GetComponent<Transform>().position = new Vector3(Mathf.Ceil(xIndex - lc.levelWidth/2.0f) - 0.5f, (lc.levelWidth / 2 + lc.levelPadding) * -1, -0.1f);
        }
        else if (xIndex < 0)
        {
            lc.xDisplayLine.GetComponent<Transform>().position = new Vector3((lc.levelWidth / 2) * 0.9f * -1, (lc.levelWidth / 2 + lc.levelPadding) * -1, -0.1f);
        }
        else if (xIndex >= lc.xWave.Length)
        {
            lc.xDisplayLine.GetComponent<Transform>().position = new Vector3((lc.levelWidth / 2) * 0.9f, (lc.levelWidth / 2 + lc.levelPadding) * -1, -0.1f);
        }

        if (yIndex > 0 && yIndex < lc.yWave.Length)
        {
            lc.yDisplayLine.GetComponent<Transform>().position = new Vector3((lc.levelHeight / 2 + lc.levelPadding) * -1, Mathf.Ceil(yIndex - lc.levelHeight / 2.0f) - 0.5f, -0.1f);
        }
        else if (yIndex < 0)
        {
            lc.yDisplayLine.GetComponent<Transform>().position = new Vector3((lc.levelHeight / 2 + lc.levelPadding) * -1, (lc.levelHeight / 2) * 0.9f * -1, -0.1f);
        }
        else if(yIndex >= lc.yWave.Length)
        {
            lc.yDisplayLine.GetComponent<Transform>().position = new Vector3((lc.levelHeight / 2 + lc.levelPadding) * -1, (lc.levelHeight / 2) * 0.9f, -0.1f);
        }

        float xVel = 0.0f;

        float yVel = 0.0f;

        if (xIndex > 0 && xIndex <= lc.xWave.Length && yIndex > 0 && yIndex <= lc.yWave.Length)
        {
            xVel = lc.xWave[xIndex - 1];
            yVel = lc.yWave[yIndex - 1];
        }

        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(xVel, yVel) * moveSpeed * dt;

        float angle = Mathf.Atan2(yVel, xVel)* Mathf.Rad2Deg;

        arrow.GetComponent<Transform>().eulerAngles = new Vector3(0.0f, 0.0f, angle);
        arrow.GetComponent<Transform>().position = gameObject.GetComponent<Transform>().position + Vector3.forward;
    }

    // Use this for initialization
    void Start()
    {
        respawnPos = gameObject.GetComponent<Transform>().position;

        yIndex = yIStart;
        xIndex = xIStart;
    }

    // Update is called once per frame
    void Update ()
    {
        MoveDot(Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.X)) gameObject.GetComponent<Transform>().position = respawnPos;
    }
}
