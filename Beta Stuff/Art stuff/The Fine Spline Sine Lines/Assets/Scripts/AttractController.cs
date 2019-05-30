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
        Vector3 direction = Vector3.zero;

        if (Vector3.Distance(transform.position, playerTransform.position) > proximity) return;

        float xDistance = Mathf.Abs(playerTransform.position.x - transform.position.x);
        float yDistance = Mathf.Abs(playerTransform.position.y - transform.position.y);

        if (xDistance > yDistance)
        {
            movement.preferedDirection += new Vector3((movement.rangeprefDir.y / xDistance) * strength, playerTransform.position.x, 0.0f);
        }
        else
        {
            movement.preferedDirection += new Vector3(playerTransform.position.x, (movement.rangeprefDir.y / yDistance) * strength, 0.0f);
        }

        Debug.Log((movement.rangeprefDir.y / xDistance) * strength);
    }
}
