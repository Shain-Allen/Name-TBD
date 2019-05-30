using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    public GameObject top;

    public GameObject bottom;

    public GameObject left;

    public GameObject right;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float topB = transform.position.y + gameObject.GetComponent<Camera>().orthographicSize / 2.0f;
        float bottomB = transform.position.y - gameObject.GetComponent<Camera>().orthographicSize / 2.0f;
        float rightB = transform.position.x - gameObject.GetComponent<Camera>().orthographicSize / 2.0f;
        float leftB = transform.position.x + gameObject.GetComponent<Camera>().orthographicSize / 2.0f;

        float bTop = top.transform.position.y;
        float bBottom = bottom.transform.position.y;
        float bRight = right.transform.position.x;
        float bLeft = left.transform.position.x;
    }
}
