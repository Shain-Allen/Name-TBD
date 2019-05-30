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

    public float changeMult = 2.0f;

    public float changeCap = 5.0f;

    [Header("Hidden")]
    private float timer = 0.0f;

    public bool isMoving = false;

    [FMODUnity.EventRef]
    public string footsteps;
    private float movementSpeed;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();

        InvokeRepeating("CallFootsteps", 0, 1);
    }

    void Update()
    {
        goal = GameObject.Find("Goal");

        prefTransform = prefered.GetComponent<Transform>();
        changeTransform = change.GetComponent<Transform>();

        goalTransform = goal.GetComponent<Transform>();

        changeDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        if (Input.GetMouseButtonDown(0) && changeMult <= changeCap)
        {
            changeMult += 0.5f;
        }
        else if (changeMult >= 2.0f)
        {
            changeMult -= 0.01f;
        }

        changeDirection = changeDirection.normalized * changeMult;
        
        if (Mathf.Approximately(changeDirection.x, Vector3.zero.x) && Mathf.Approximately(changeDirection.y, Vector3.zero.y))
        {
            isMoving = false;
        }
        else
        {
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

        preferedDirection = preferedDirection.normalized;

        gameObject.GetComponent<Rigidbody2D>().velocity = (changeDirection + preferedDirection) * speed * Time.deltaTime;

        

        float preferedAngle = Mathf.Atan2(preferedDirection.y, preferedDirection.x) * Mathf.Rad2Deg;

        float changeAngle = Mathf.Atan2(changeDirection.y, changeDirection.x) * Mathf.Rad2Deg;

        prefered.GetComponent<Transform>().eulerAngles = new Vector3(0.0f, 0.0f, preferedAngle);

        change.GetComponent<Transform>().eulerAngles = new Vector3(0.0f, 0.0f, changeAngle);

        preferedDirection = Vector3.zero;

        Rigidbody2D MyRigibody = gameObject.GetComponent<Rigidbody2D>();

        movementSpeed = (Mathf.Abs((MyRigibody.velocity.x + MyRigibody.velocity.y) / 2.0f));

        //animation is controlled here
        Rigidbody2D direction = gameObject.GetComponent<Rigidbody2D>();

        anim.SetFloat("MoveX", direction.velocity.x);
        anim.SetFloat("MoveY", direction.velocity.y);
    }

    void CallFootsteps()
    {
        if (isMoving == true)
        {
            FMODUnity.RuntimeManager.PlayOneShot(footsteps);
        }
    }
}
