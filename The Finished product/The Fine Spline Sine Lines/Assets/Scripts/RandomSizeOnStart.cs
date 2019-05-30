using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSizeOnStart : MonoBehaviour
{
    public float max = 0.0f;

    public float min = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        float size = Random.Range(min, max);

        Transform t = gameObject.GetComponent<Transform>();

        t.localScale = new Vector3(t.localScale.x + size, t.localScale.y + size, 1.0f);
    }
}
