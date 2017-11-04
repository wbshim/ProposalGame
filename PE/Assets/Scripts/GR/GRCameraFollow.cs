using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GRCameraFollow : MonoBehaviour {
    Camera cam;
    public float FOVSmooth;
    CPC_CameraPath cameraPath;

    public float WaitTime;

	// Use this for initialization
	void Start () {
        cam = Camera.main;
        cameraPath = cam.GetComponent<CPC_CameraPath>();
	}

    private void OnTriggerEnter(Collider col)
    {
        //cameraPath.enabled = false;
        //Debug.Log("Turned cameraPath off: " + cameraPath.enabled);
        //StartCoroutine(playIntro());
    }
    private void OnTriggerExit(Collider col)
    {
        Debug.Log("Camera exited last waypoint");
        Debug.Log(col.name);
        StartCoroutine(changeFOV(cam, 30f, 60f));
    }

    IEnumerator changeFOV(Camera _cam, float startFOV, float targetFOV)
    {
        Debug.Log("started changing FOV");
        float currentFOV = startFOV;
        while(currentFOV < targetFOV)
        {
            cam.fieldOfView = currentFOV;
            currentFOV = Mathf.Lerp(currentFOV, targetFOV + 5f, FOVSmooth * Time.deltaTime);
            yield return null;
        }
        currentFOV = targetFOV;
    }
    private IEnumerator playIntro()
    {
        cameraPath.PausePath();
        yield return new WaitForSeconds(WaitTime);
        cameraPath.ResumePath();
    }
}
