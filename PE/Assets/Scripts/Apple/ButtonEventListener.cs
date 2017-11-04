using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonEventListener : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
    public bool buttonPressed;
    //public Transform Player;
    //AppleWonbo playerController;

    private void Start()
    {
        //playerController = Player.GetComponent<AppleWonbo>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonPressed = true;
        Debug.Log("Button pressed");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonPressed = false;
        Debug.Log("Button released");
    }
}
