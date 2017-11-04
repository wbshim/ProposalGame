using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class VegasGameManager : MonoBehaviour {

    public int MaxNumNPCs;
    public Transform NPCHolder;
    public Transform[] NPCs;
    public float MinXPos = -30f;
    public float MaxXPos = 30f;
    public float MinZPos = -10f;
    public float MaxZPos = 20f;
    public List<Transform> NPCList = new List<Transform>();
    float newNPCXPos;
    float newNPCZpos;
    Vector3 newNPCPosition;
    Transform newNPC;
    public float Space; // Space between NPCs
    public Vector3 Center; // Center
    public float CenterSpace; // Amount of space to leave empty for players

    // Camera controller
    CPC_CameraPath cameraPath;

    // UI
    VegasUIManager uiManager;

    // Score
    public int TargetScore;
    public int CurrentScore;

    // Music Tempo
    public float BPM;

    // Game over
    bool gameOver = false;

    private void Awake()
    {
        uiManager = GameObject.Find("Canvas").GetComponent<VegasUIManager>();
    }
    void Start ()
    {
        NPCHolder.gameObject.SetActive(false);
        uiManager.PlayIntro();
        StartCoroutine(SpawnNPCs());
        Camera cam = Camera.main;
        cameraPath = (CPC_CameraPath)cam.GetComponent<CPC_CameraPath>();
    }

    IEnumerator SpawnNPCs()
    {
        float z = MinZPos;
        int numNPCs = 0;
        while(z <= MaxZPos)
        {
            float x;
            if(z%2==0) // If z row is even
            {
                x = MinXPos;
            }
            else
            {
                x = MinXPos + Space/2;
            }
            while(x <= MaxXPos)
            {
                newNPCPosition = new Vector3(x, 0, z);
                if(Vector3.Distance(newNPCPosition, Center) >= CenterSpace)
                {
                    Quaternion rotation = Quaternion.LookRotation(Center - newNPCPosition);
                    int i = Random.Range(0, 2);
                    newNPC = (Transform)Instantiate(NPCs[i], newNPCPosition, rotation);
                    newNPC.parent = NPCHolder;
                    numNPCs++;
                    Debug.Log("Number of NPCs: " + numNPCs.ToString());
                }
                yield return null;
                x += Space;
            }
            z += Space;
            yield return null;
        }
        NPCHolder.gameObject.SetActive(true);
        cameraPath.PlayPath(5f);
        StartCoroutine(StartDialogue(7f));
    }
    IEnumerator StartDialogue(float t)
    {
        yield return new WaitForSeconds(t);
        FindObjectOfType<DialogueRunner>().StartDialogue("Start");
    }
    [YarnCommand("beginGameplay")]
    public void BeginGameplay()
    {
        uiManager.ShowControls();
    }
    public void UpdateScore(int _score)
    {
        CurrentScore += _score;
        uiManager.UpdateProgressBar(CurrentScore);
        // If CurrentScore >= TargetScore play win card
        if (CurrentScore >= TargetScore)
        {
            if (!gameOver)
            {
                gameOver = true;
                FindObjectOfType<AudioManager>().Play("Cheer", 0.25f);
                uiManager.PlayEndCard();
            }
        }

    }
}
