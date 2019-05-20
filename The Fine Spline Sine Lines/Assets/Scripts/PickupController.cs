using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    public enum pickUpType { Mult, Div, IndexShift, Invert};

    public pickUpType Type = pickUpType.Mult;

    public GameObject levelController;

    void Div()
    {
        float[] xW = levelController.GetComponent<LevelController>().xWaveF;

        int xL = xW.Length;

        float[] yW = levelController.GetComponent<LevelController>().yWaveF;

        int yL = yW.Length;

        for (int i = 0; i < xL; ++i)
        {
            xW[i] /= 2;
            yW[i] /= 2;

            xW[i] = Mathf.Clamp(xW[i], -1.0f, 1.0f);
            yW[i] = Mathf.Clamp(yW[i], -1.0f, 1.0f);
        }
    }

    void Mult()
    {
        float[] xW = levelController.GetComponent<LevelController>().xWaveF;

        int xL = xW.Length;

        float[] yW = levelController.GetComponent<LevelController>().yWaveF;

        int yL = yW.Length;

        for (int i = 0; i < xL; ++i)
        {
            xW[i] += Random.Range(-0.25f, 0.25f);
            yW[i] += Random.Range(-0.25f, 0.25f);

            xW[i] = Mathf.Clamp(xW[i], -1.0f, 1.0f);
            yW[i] = Mathf.Clamp(yW[i], -1.0f, 1.0f);
        }
    }

    void Invert()
    {
        float[] xW = levelController.GetComponent<LevelController>().xWaveF;

        int xL = xW.Length;

        float[] yW = levelController.GetComponent<LevelController>().yWaveF;

        int yL = yW.Length;

        for (int i = 0; i < xL; ++i)
        {
            xW[i] *= -1;
            yW[i] *= -1;

            xW[i] = Mathf.Clamp(xW[i], -1.0f, 1.0f);
            yW[i] = Mathf.Clamp(yW[i], -1.0f, 1.0f);
        }
    }

    void IndexShift()
    {
        DotController dC = GameObject.Find("Dot").GetComponent<DotController>();

        dC.xIndex = Random.Range(0, levelController.GetComponent<LevelController>().);
        dC.yIndex = Random.Range(0, 9);
    }

	// Use this for initialization
	void Start ()
    {
        levelController = GameObject.FindGameObjectWithTag("Level");
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D (Collider2D collider2D)
    {
        if (collider2D.name != "Dot") return;

        levelController = GameObject.FindGameObjectWithTag("Level");

        if (Type == pickUpType.Div)
        {
            Div();
        }
        else if (Type == pickUpType.Mult)
        {
            Mult();
        }
        else if (Type == pickUpType.Invert)
        {
            Invert();
        }
        else if (Type == pickUpType.IndexShift)
        {
            IndexShift();
        }

        Destroy(gameObject);
	}
}
