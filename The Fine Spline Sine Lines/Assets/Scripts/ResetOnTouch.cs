using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetOnTouch : MonoBehaviour
{
	// Update is called once per frame
	void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.gameObject.name != "Player") return;

        GameObject.Find("Camera").GetComponent<LevelManager>().Reloadlevel();
	}
}
