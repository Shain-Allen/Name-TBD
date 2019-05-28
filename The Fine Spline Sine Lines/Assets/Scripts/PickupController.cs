using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    public Vector2 amount = Vector2.zero;
	
	void OnTriggerEnter2D (Collider2D collider2D)
    {
        if (collider2D.name != "Player") return;

        collider2D.GetComponent<Movement>().rangeChangeDir -= amount;

        Destroy(gameObject);
	}
}
