using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public GameObject dot;

    private float progT = 0.0f;

    [Header("Level Settings")]

    public float levelPadding = 1.5f;

    public float levelWidth = 10.0f;

    public float levelHeight = 10.0f;

    public bool hasCollectible = false;

    public float time = 0.0f;

    public float[] xWave;

    public float[] xWaveF;

    public float[] yWave;

    public float[] yWaveF;

    public GameObject[] xLines;

    public GameObject[] yLines;

    public GameObject[] xIndicators;

    public GameObject[] yIndicators;

    public float debugScrollMult = 0.25f;

    private bool debug = false;

    private bool hasMoved = false;

    [Header("Borders")]

    public Transform top;
    public Transform bottom;
    public Transform left;
    public Transform right;

    [Header("Prefabs")]

    public GameObject prefabLine;

    public GameObject prefabSquare;

    public GameObject prefabBackground;

    public GameObject prefabGuide;

    public GameObject prefabIndicator;

    public GameObject xDisplayLine;
    public GameObject yDisplayLine;

    public GameObject xBackground;
    public GameObject yBackground;

    public GameObject guide;

    public Color xColor = Color.red;

    public Color yColor = Color.green;
    
    public void CreateWaves()
    {
        for (int i = 0; i < xWave.Length - 1; i++)
        {
            Vector3 startP = new Vector3(i - xWave.Length / 2 + 0.5f, (levelHeight / 2 + levelPadding) * -1 + xWave[i]);

            Vector3 endP = new Vector3((i + 1) - xWave.Length / 2 + 0.5f, (levelHeight / 2 + levelPadding) * -1 + xWave[i + 1]);

            //Debug.DrawLine(startP, endP, Color.blue, 1000.0f);

            xLines[i] = Instantiate(prefabLine, startP + (endP - startP) / 2, dot.GetComponent<Transform>().rotation);

            float angle = Mathf.Atan2(startP.y - endP.y, startP.x - endP.x);

            xLines[i].GetComponent<Transform>().eulerAngles = new Vector3(0.0f, 0.0f, angle * 180 / Mathf.PI + 90.0f);
            xLines[i].GetComponent<Transform>().localScale = new Vector3(0.2f, Vector3.Distance(startP, endP) * 1.625f, 1.0f);
            xLines[i].GetComponent<SpriteRenderer>().color = xColor;
            xLines[i].name = "XLine " + i + " -> " + xWave[i];
        }

        for (int i = 0; i < yWave.Length - 1; i++)
        {
            Vector3 startP = new Vector3((levelWidth / 2 + levelPadding) * -1 + yWave[i], i - yWave.Length / 2 + 0.5f);

            Vector3 endP = new Vector3((levelWidth / 2 + levelPadding) * -1 + yWave[i + 1], (i + 1) - yWave.Length / 2 + 0.5f);

            //Debug.DrawLine(startP, endP, Color.red, 1000.0f);

            yLines[i] = Instantiate(prefabLine, startP + (endP - startP) / 2, dot.GetComponent<Transform>().rotation);

            float angle = Mathf.Atan2(startP.y - endP.y, startP.x - endP.x);

            yLines[i].GetComponent<Transform>().eulerAngles = new Vector3(0.0f, 0.0f, angle * 180 / Mathf.PI + 90.0f);
            yLines[i].GetComponent<Transform>().localScale = new Vector3(0.2f, Vector3.Distance(startP, endP) * 1.625f, 1.0f);
            yLines[i].GetComponent<SpriteRenderer>().color = yColor;
            yLines[i].name = "YLine " + i + " -> " + yWave[i];
        }
    }

    private void AlignWave(ref float[] wave, ref float[] waveF)
    {
        for (int i = 0; i < wave.Length - 1; i++)
        {
            Vector3 startP = new Vector3(i - wave.Length / 2 + 0.5f, (levelHeight / 2 + levelPadding) * -1 + wave[i]);

            Vector3 endP = new Vector3((i + 1) - wave.Length / 2 + 0.5f, (levelHeight / 2 + levelPadding) * -1 + wave[i + 1]);

            Vector3 startPF = new Vector3(i - waveF.Length / 2 + 0.5f, (levelHeight / 2 + levelPadding) * -1 + waveF[i]);

            Vector3 endPF = new Vector3((i + 1) - waveF.Length / 2 + 0.5f, (levelHeight / 2 + levelPadding) * -1 + waveF[i + 1]);

            float angle = Mathf.Atan2(startP.y - endP.y, startP.x - endP.x);

            float angleF = Mathf.Atan2(startPF.y - endPF.y, startPF.x - endPF.x);

            Transform t = xLines[i].GetComponent<Transform>();

            t.eulerAngles = Vector3.Lerp(new Vector3(0.0f, 0.0f, angle * 180 / Mathf.PI + 90.0f), new Vector3(0.0f, 0.0f, angleF * 180 / Mathf.PI + 90.0f), progT);
            t.localScale = Vector3.Lerp(new Vector3(0.2f, Vector3.Distance(startP, endP) * 1.625f, 1.0f), new Vector3(0.2f, Vector3.Distance(startPF, endPF) * 1.625f, 1.0f), progT);
            t.position = Vector3.Lerp(startP + (endP - startP) / 2, startPF + (endPF - startPF) / 2, progT);
            Mathf.Lerp(wave[i], waveF[i], progT);
        }
    }

    public void AlignWaves()
    {
        if (xWave == xWaveF) return;
        if (yWave == yWaveF) return;

        progT += Time.deltaTime;
        if (progT >= 10.0f)
        {
            progT = 0.0f;
        }

        Debug.Log(progT);
        
        AlignWave(ref xWave, ref xWaveF);

        AlignWave(ref yWave, ref yWaveF);
    }

    public void EraseWaves()
    {
        for (int i = 0; i < xLines.Length; i++)
        {
            Destroy(xLines[i]);
        }

        for (int i = 0; i < yLines.Length; i++)
        {
            Destroy(yLines[i]);
        }
    }

    public void AdjustLevel()
    {
        top.position = new Vector3(0.0f, levelHeight / 2, 0.0f);
        bottom.position = new Vector3(0.0f, (levelHeight / 2) * -1, 0.0f);
        right.position = new Vector3(levelHeight / 2, 0.0f, 0.0f);
        left.position = new Vector3((levelHeight / 2) * -1, 0.0f, 0.0f);

        top.localScale = new Vector3(levelHeight + 0.5f, 0.5f, 1.0f);
        bottom.localScale = new Vector3((levelHeight + 0.5f) * -1, 0.5f, 1.0f);
        right.localScale = new Vector3(0.5f, levelHeight + 0.5f, 1.0f);
        left.localScale = new Vector3(0.5f, (levelHeight + 0.5f) * -1, 1.0f);
    }

    void OnDestroy()
    {
        EraseWaves();

        for (int i = 0; i < xIndicators.Length; i++)
        {
            Destroy(xIndicators[i]);
        }

        for (int i = 0; i < yIndicators.Length; i++)
        {
            Destroy(yIndicators[i]);
        }

        Destroy(xBackground);
        Destroy(yBackground);
        Destroy(guide);
    }
    
    void Start ()
    {
        levelHeight = yWave.Length;
        levelWidth = xWave.Length;

        xBackground = Instantiate(prefabBackground, new Vector3((levelHeight / 2 + levelPadding) * -1, 0.0f, 0.1f), gameObject.GetComponent<Transform>().rotation);
        xBackground.GetComponent<Transform>().localScale = new Vector3(3.5f, levelWidth + (levelWidth / 2) * 0.85f, 1.0f);

        yBackground = Instantiate(prefabBackground, new Vector3(0.0f, (levelHeight / 2 + levelPadding) * -1, 0.1f), gameObject.GetComponent<Transform>().rotation);
        yBackground.GetComponent<Transform>().localScale = new Vector3(3.5f, levelHeight + (levelHeight / 2) * 0.85f, 1.0f);
        yBackground.GetComponent<Transform>().eulerAngles = new Vector3(0.0f, 0.0f, 90.0f);

        xIndicators = new GameObject[xWave.Length];
        yIndicators = new GameObject[yWave.Length];

        for (int i = 0; i < xWave.Length; i++)
        {
            Vector3 startP = new Vector3(i - xWave.Length / 2, (levelHeight / 2 + levelPadding) * -1);

            Vector3 endP = new Vector3((i + 1) - xWave.Length / 2, (levelHeight / 2 + levelPadding) * -1);

            xIndicators[i] = Instantiate(prefabIndicator, startP + (endP - startP) / 2, xBackground.GetComponent<Transform>().rotation);
        }

        for (int i = 0; i < yWave.Length; i++)
        {
            Vector3 startP = new Vector3((levelWidth / 2 + levelPadding) * -1, i - yWave.Length / 2);

            Vector3 endP = new Vector3((levelWidth / 2 + levelPadding) * -1, (i + 1) - yWave.Length / 2);

            yIndicators[i] = Instantiate(prefabIndicator, startP + (endP - startP) / 2, yBackground.GetComponent<Transform>().rotation);
        }

        guide = Instantiate(prefabGuide, new Vector3((levelHeight / 2 + levelPadding) * -1, (levelHeight / 2 + levelPadding) * -1, 0.0f), gameObject.GetComponent<Transform>().rotation);

        xLines = new GameObject[xWave.Length-1];
        yLines = new GameObject[yWave.Length-1];

        xWaveF = xWave;
        yWaveF = yWave;

        AdjustLevel();
        CreateWaves();
    }

    void ClampWave(ref float[] wave)
    {
        for (int i = 0; i < wave.Length; ++i)
        {
            wave[i] = Mathf.Clamp(wave[i], -1.0f, 1.0f);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        ClampWave(ref xWave);
        ClampWave(ref yWave);
        ClampWave(ref xWaveF);
        ClampWave(ref yWaveF);

        Transform t = dot.GetComponent<Transform>();

        if (!hasMoved && !Mathf.Approximately(t.position.x, dot.GetComponent<DotController>().respawnPos.x) && !Mathf.Approximately(t.position.y, dot.GetComponent<DotController>().respawnPos.y)) hasMoved = true;

        if (hasMoved) time += Time.deltaTime;

        AlignWaves();

        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log(xWave[0]);

            Debug.Log(xWaveF[0]);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKey(KeyCode.C))
        {
            debug = !debug;
            Debug.Log(debug);
        }
        else if (!debug) return;

        if (debug && Input.GetKey(KeyCode.Alpha1))
        {
            if (Input.GetKey(KeyCode.X))
            {
                xWaveF[0] += Input.mouseScrollDelta.y * debugScrollMult;
            }
            else if (Input.GetKey(KeyCode.Y))
            {
                yWaveF[0] += Input.mouseScrollDelta.y * debugScrollMult;
            }
        }

        if (debug && Input.GetKey(KeyCode.Alpha2))
        {
            if (Input.GetKey(KeyCode.X))
            {
                xWaveF[1] += Input.mouseScrollDelta.y * debugScrollMult;
            }
            else if (Input.GetKey(KeyCode.Y))
            {
                yWaveF[1] += Input.mouseScrollDelta.y * debugScrollMult;
            }
        }

        if (debug && Input.GetKey(KeyCode.Alpha3))
        {
            if (Input.GetKey(KeyCode.X))
            {
                xWaveF[2] += Input.mouseScrollDelta.y * debugScrollMult;
            }
            else if (Input.GetKey(KeyCode.Y))
            {
                yWaveF[2] += Input.mouseScrollDelta.y * debugScrollMult;
            }
        }

        if (debug && Input.GetKey(KeyCode.Alpha4))
        {
            if (Input.GetKey(KeyCode.X))
            {
                xWaveF[3] += Input.mouseScrollDelta.y * debugScrollMult;
            }
            else if (Input.GetKey(KeyCode.Y))
            {
                yWaveF[3] += Input.mouseScrollDelta.y * debugScrollMult;
            }
        }

        if (debug && Input.GetKey(KeyCode.Alpha5))
        {
            if (Input.GetKey(KeyCode.X))
            {
                xWaveF[4] += Input.mouseScrollDelta.y * debugScrollMult;
            }
            else if (Input.GetKey(KeyCode.Y))
            {
                yWaveF[4] += Input.mouseScrollDelta.y * debugScrollMult;
            }
        }

        if (debug && Input.GetKey(KeyCode.Alpha6))
        {
            if (Input.GetKey(KeyCode.X))
            {
                xWaveF[5] += Input.mouseScrollDelta.y * debugScrollMult;
            }
            else if (Input.GetKey(KeyCode.Y))
            {
                yWaveF[5] += Input.mouseScrollDelta.y * debugScrollMult;
            }
        }

        if (debug && Input.GetKey(KeyCode.Alpha7))
        {
            if (Input.GetKey(KeyCode.X))
            {
                xWaveF[6] += Input.mouseScrollDelta.y * debugScrollMult;
            }
            else if (Input.GetKey(KeyCode.Y))
            {
                yWaveF[6] += Input.mouseScrollDelta.y * debugScrollMult;
            }
        }

        if (debug && Input.GetKey(KeyCode.Alpha8))
        {
            if (Input.GetKey(KeyCode.X))
            {
                xWaveF[7] += Input.mouseScrollDelta.y * debugScrollMult;
            }
            else if (Input.GetKey(KeyCode.Y))
            {
                yWaveF[7] += Input.mouseScrollDelta.y * debugScrollMult;
            }
        }

        if (debug && Input.GetKey(KeyCode.Alpha9))
        {
            if (Input.GetKey(KeyCode.X))
            {
                xWaveF[8] += Input.mouseScrollDelta.y * debugScrollMult;
            }
            else if (Input.GetKey(KeyCode.Y))
            {
                yWaveF[8] += Input.mouseScrollDelta.y * debugScrollMult;
            }
        }

        if (debug && Input.GetKey(KeyCode.Alpha0))
        {
            if (Input.GetKey(KeyCode.X))
            {
                xWaveF[9] += Input.mouseScrollDelta.y * debugScrollMult;
            }
            else if (Input.GetKey(KeyCode.Y))
            {
                yWaveF[9] += Input.mouseScrollDelta.y * debugScrollMult;
            }
        }
    }
}
