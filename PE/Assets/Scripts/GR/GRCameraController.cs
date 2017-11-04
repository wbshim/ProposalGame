using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GRCameraController : MonoBehaviour {

    public float TitleWaitTime;
    public float WaitTime;
    public Transform Target;
    public Transform Player;
    public Canvas canvas;

    Camera cam;
    CPC_CameraPath cameraPath;
    UIManager uiManager;
    Kayak kayak;

    float currentTimeInWaypoint;
    int currentWaypoint;
    bool introPlayed;
    bool introPanEnded;

	// Use this for initialization
	void Start () {
        cam = Camera.main;
        cameraPath = (CPC_CameraPath) cam.GetComponent<CPC_CameraPath>();
        StartCoroutine(delayedStart(WaitTime));
        introPlayed = false;
        introPanEnded = false;
        uiManager = canvas.GetComponent<UIManager>();
        kayak = Player.GetComponent<Kayak>();
    }
	
	// Update is called once per frame
	void Update () {

        currentWaypoint = cameraPath.GetCurrentWayPoint();
        currentTimeInWaypoint = cameraPath.GetCurrentTimeInWaypoint();
        
        if(!introPanEnded)
        {
            if (currentWaypoint == 1)
            {
                if (Mathf.FloorToInt(currentTimeInWaypoint) == 0 && introPlayed == false)
                {
                    StartCoroutine(playIntro(TitleWaitTime));
                }
            }
            if (currentWaypoint == 2)
            {
                Debug.Log("intro sequence ended");
                cam.transform.parent = Target;
                kayak.BeginDialogue();
                introPanEnded = true;
            }
        }
        
    }
    IEnumerator delayedStart(float waitTime)
    {
        Debug.Log("Beginning delayedStart");
        yield return new WaitForSeconds(waitTime);
        cameraPath.PlayPath(10f);
        //cameraPath.ResumePath();
        Debug.Log(cameraPath.playOnAwake);
    }
    IEnumerator playIntro(float waitTime)
    {
        introPlayed = true;
        cameraPath.PausePath();
        uiManager.PlayTitleIntro();
        yield return new WaitForSeconds(waitTime);
        cameraPath.ResumePath();
    }
}
