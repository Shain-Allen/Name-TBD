using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpriteOnTimer : MonoBehaviour
{
    public Sprite[] sprites;

    public float timeBeforeSwitch = 0.5f;

    public int currentSprite = 0;

    private float timer = 0.0f;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeBeforeSwitch)
        {
            if (currentSprite < sprites.Length-1)
            {
                ++currentSprite;
            }
            else
            {
                currentSprite = 0;
            }
            timer = 0.0f;
        }

        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[currentSprite];
    }
}
