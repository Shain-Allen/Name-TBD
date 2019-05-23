using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractController : MonoBehaviour
{
    public GameObject player;

    private new Transform transform;

    private Movement movement;

    public float multiplier;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        movement = player.GetComponent<Movement>();
        transform = player.GetComponent<Transform>();

        float distanceX = transform.position.x - gameObject.GetComponent<Transform>().position.x;
        float distanceY = transform.position.y - gameObject.GetComponent<Transform>().position.y;

        if (distanceX < distanceY)
        {
            //add x to preferedDirection's x * 1 / distance
        }
        else
        {
            //add y to preferedDirection's y * 1 / distance
        }
    }
}
