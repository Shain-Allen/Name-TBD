using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject prefered;

    public GameObject change;

    public GameObject goal;

    public GameObject collectible;

    public Transform prefTransform;

    public Transform changeTransform;

    public Transform goalTransform;

    public Transform collectTransform;

    [Header("Directions")]

    public Vector3 preferedDirection = Vector3.zero;

    public Vector3 changeDirection = Vector3.zero;

    [Header("Ranges")]

    public Vector2 rangeChangeDir = Vector2.zero;

    public Vector2 rangeprefDir = Vector2.zero;

    [Header("Speed")]

    public float speed = 10.0f;

    public float tSpeed = 0.1f;

    public float changeAmount = 0.1f;

    public float changeTime = 1.0f;

    [Header("Hidden")]
    private float timer = 0.0f;

    private float tX = 0.0f;
    private float tY = 0.0f;

    public bool isMoving = false;

    [FMODUnity.EventRef]
    public string footsteps;
    private float movementSpeed;

    private void Start()
    {
        collectible = GameObject.Find("Collectible");
        collectTransform = collectible.GetComponent<Transform>();

        InvokeRepeating("CallFootsteps", 0, movementSpeed);
    }

    void Update()
    {
        goal = GameObject.Find("Goal");

        prefTransform = prefered.GetComponent<Transform>();
        changeTransform = change.GetComponent<Transform>();

        goalTransform = goal.GetComponent<Transform>();


        if (tX >= 1.0f) tX = 1.0f;
        else tX += Time.deltaTime * tSpeed;

        if (tY >= 1.0f) tY = 1.0f;
        else tY += Time.deltaTime * tSpeed;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            changeDirection.y += changeAmount;
            tY = 0.0f;
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            changeDirection.y -= changeAmount;
            tY = 0.0f;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            changeDirection.x += changeAmount;
            tX = 0.0f;
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            changeDirection.x -= changeAmount;
            tX = 0.0f;
        }

        if (!(Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftArrow)) && !(Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.RightArrow)))
        {
            changeDirection.x = Mathf.Lerp(changeDirection.x, 0.0f, tX);
        }

        if (!(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.UpArrow)) && !(Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.DownArrow)))
        {
            changeDirection.y = Mathf.Lerp(changeDirection.y, 0.0f, tY);
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

        if (Mathf.Approximately(preferedDirection.x, Vector3.zero.x) && Mathf.Approximately(preferedDirection.y, Vector3.zero.y))
        {
            prefered.SetActive(false);
        }
        else
        {
            prefered.SetActive(true);
        }

        preferedDirection = new Vector3(Mathf.Clamp(preferedDirection.x, rangeprefDir.x, rangeprefDir.y), Mathf.Clamp(preferedDirection.y, rangeprefDir.x, rangeprefDir.y), 0.0f);

        changeDirection = new Vector3(Mathf.Clamp(changeDirection.x, rangeChangeDir.x, rangeChangeDir.y), Mathf.Clamp(changeDirection.y, rangeChangeDir.x, rangeChangeDir.y), 0.0f);

        gameObject.GetComponent<Rigidbody2D>().velocity = (changeDirection + preferedDirection) * speed * Time.deltaTime;

        float preferedAngle = Mathf.Atan2(preferedDirection.y, preferedDirection.x) * Mathf.Rad2Deg;

        float changeAngle = Mathf.Atan2(changeDirection.y, changeDirection.x) * Mathf.Rad2Deg;

        prefered.GetComponent<Transform>().eulerAngles = new Vector3(0.0f, 0.0f, preferedAngle);

        change.GetComponent<Transform>().eulerAngles = new Vector3(0.0f, 0.0f, changeAngle);

        Rigidbody2D MyRigibody = gameObject.GetComponent<Rigidbody2D>();

        Debug.Log(isMoving);

        movementSpeed = Mathf.Abs((MyRigibody.velocity.x + MyRigibody.velocity.y) / 2.0f);
    }

    void CallFootsteps()
    {
        if(isMoving == true)
        {
            FMODUnity.RuntimeManager.PlayOneShot(footsteps);
        }
    }
}
