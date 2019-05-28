using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractController : MonoBehaviour
{
    public GameObject[] attracts;

    private new Transform transform;

    private Movement movement;

    public float distMult = 2.0f;

    public float boundsMult = 0.1f;

    // Update is called once per frame
    void Update()
    {
        movement = gameObject.GetComponent<Movement>();
        transform = gameObject.GetComponent<Transform>();
        Vector3 direction = Vector3.zero;

        attracts = GameObject.FindGameObjectsWithTag("Reset");

        foreach (GameObject obj in attracts)
        {
            if (obj.name.Contains("Reset"))
            {
                direction += (obj.GetComponent<Transform>().position - transform.position) * (1.0f / Vector3.Distance(obj.GetComponent<Transform>().position, transform.position) * distMult);
            }
            else
            {
                direction += (transform.position - obj.GetComponent<Transform>().position) * (1.0f / Vector3.Distance(obj.GetComponent<Transform>().position, transform.position) * boundsMult);
            }
        }



        movement.preferedDirection = direction;
    }
}
