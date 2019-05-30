using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractController : MonoBehaviour
{
    private Transform playerTransform;

    private GameObject player;

    private Movement movement;

    public float proximity = 2.0f;

    public float strength = 10.0f;

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("Player");
        playerTransform = player.GetComponent<Transform>();
        movement = player.GetComponent<Movement>();


        if (Vector3.Distance(transform.position, playerTransform.position) > proximity) return;

        Vector3 direction = transform.position - playerTransform.position;

        movement.preferedDirection += (direction.normalized / Vector3.Distance(transform.position, playerTransform.position)) * strength;
    }
}
