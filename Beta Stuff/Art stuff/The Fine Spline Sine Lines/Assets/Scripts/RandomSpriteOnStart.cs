using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpriteOnStart : MonoBehaviour
{
    public Sprite[] sprites;

    // Start is called before the first frame update
    void Start()
    {
        if (sprites == null) return;
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
    }
}
