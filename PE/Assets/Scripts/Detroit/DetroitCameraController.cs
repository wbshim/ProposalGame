using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetroitCameraController : MonoBehaviour {

    public Transform GameManager;
    public float TitleWaitTime;
    public float WaitTime;
    public Transform Target;
    public Quaternion TargetRotation;
    public Canvas canvas;

    Camera cam;
    CPC_CameraPath cameraPath;
    DetroitUIManager uiManager;
    DetroitGameManager gameManager;

    float currentTimeInWaypoint;
    int currentWaypoint;
    bool introPlayed;
    bool introPanEnded;

    // Variables for camera follow
    public bool FollowTargetForward = false;
    public bool FollowingTarget;
    public float Smooth;
    public Vector3 Offset;
    Vector3 targetPosition;
    Vector3 smoothedPosition;
    Quaternion smoothedRotation;


    // Use this for initialization
    void Start()
    {
        cam = Camera.main;
        cameraPath = (CPC_CameraPath)cam.GetComponent<CPC_CameraPath>();
        introPlayed = false;
        introPanEnded = false;
        uiManager = canvas.GetComponent<DetroitUIManager>();
        gameManager = GameManager.GetComponent<DetroitGameManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

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
                introPanEnded = true;
                //cam.transform.parent = Target;
                gameManager.StartGamePlay();
            }
        }
        else
        {
            // Smooth position
            targetPosition = Target.position + Offset;
            smoothedPosition = Vector3.Lerp(transform.position, targetPosition, Smooth * Time.fixedDeltaTime);
            transform.position = smoothedPosition;
            transform.rotation = Quaternion.Lerp(transform.rotation, TargetRotation, Smooth * Time.fixedDeltaTime);
        }
    }

    public void SetTarget(Transform _target, Vector3 _offset, Quaternion _targetRotation)
    {
        Target = _target;
        Offset = _offset;
        TargetRotation = _targetRotation;
    }


    IEnumerator delayedStart(float waitTime)
    {
        Debug.Log("Beginning delayedStart");
        yield return new WaitForSeconds(waitTime);
        cameraPath.PlayPath(10f);
        //cameraPath.ResumePath();
        Debug.Log(cameraPath.playOnAwake);
    }
    public void BeginCameraPath()
    {
        cameraPath.PlayPath(10f);
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
