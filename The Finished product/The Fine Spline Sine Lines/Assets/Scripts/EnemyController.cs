using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 2.0f;

    public float proximity = 2.0f;

    public Vector3 restartPos = Vector3.zero;

    private GameObject dot;

    // Start is called before the first frame update
    void Start()
    {
        restartPos = gameObject.GetComponent<Transform>().position;

        dot = GameObject.Find("Dot");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != dot) return;

        gameObject.GetComponent<Transform>().position = restartPos;
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        Transform pos = gameObject.GetComponent<Transform>();

        Transform dPos = dot.GetComponent<Transform>();

        if (Vector3.Distance(dPos.position, pos.position) <= proximity)
        {
            Vector3 dir = dPos.position - pos.position;

            gameObject.GetComponent<Rigidbody2D>().velocity += new Vector2(dir.x, dir.y).normalized *speed * Time.deltaTime;
        }
    }
}
