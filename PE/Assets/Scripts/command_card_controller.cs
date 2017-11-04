using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class command_card_controller : MonoBehaviour {
    Camera cameraMain;
    // Use this for initialization
    void Start () {
        cameraMain = Camera.main;
    }

    public void focusPicture()
    {
        cameraMain.GetComponent<CameraController>().focusPicture();
    }
}
