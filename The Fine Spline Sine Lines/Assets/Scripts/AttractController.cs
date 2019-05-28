using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractController : MonoBehaviour
{
    private Transform playerTransform;

    private GameObject player;

    private Movement movement;

    public float proximity = 2.0f;

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("Player");
        playerTransform = player.GetComponent<Transform>();
        movement = player.GetComponent<Movement>();
        Vector3 direction = Vector3.zero;

        if (Vector3.Distance(transform.position, playerTransform.position) > proximity) return;

        float xDistance = Mathf.Abs(transform.position.x - playerTransform.position.x);
        float yDistance = Mathf.Abs(transform.position.y - playerTransform.position.y);

        if (xDistance > yDistance)
        {
            movement.preferedDirection.x += movement.rangeprefDir.y / xDistance;
        }
        else
        {
            movement.preferedDirection.y += movement.rangeprefDir.y / yDistance;
        }
    }
}
