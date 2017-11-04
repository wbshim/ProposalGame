using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

[RequireComponent(typeof(PlayerMotorStarbucks))]
public class PlayerStarbucks : MonoBehaviour {

    public LayerMask movementMask;
    public StarbucksJiYeon focus;
    Camera cam;
    PlayerMotorStarbucks motor;
    UIManager uiManager;

	// Use this for initialization
	void Start () {
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        cam = Camera.main;
        motor = GetComponent<PlayerMotorStarbucks>();
        StartCoroutine(ShowTitle());
        StartCoroutine(StartDialogue(8));
    }
    IEnumerator ShowTitle()
    {
        yield return new WaitForSeconds(4);
        uiManager.PlayTitleIntro();
    }
	IEnumerator StartDialogue(float t)
    {
        yield return new WaitForSeconds(t);
        FindObjectOfType<DialogueRunner>().StartDialogue("Wonbo");
    }
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 100, movementMask))
            {
                motor.MoveToPoint(hit.point);
                // Move player to what we hit
                Debug.Log(hit.transform.name);
                if(hit.transform.tag == "JiYeon")
                {
                    FindObjectOfType<AudioManager>().Play("Win",0.2f);
                    Button arrow = FindObjectOfType<StarbucksGameManager>().arrow;
                    if(arrow.gameObject.activeSelf)
                        arrow.StopArrowIndicator();
                    StarbucksJiYeon jiYeon = hit.collider.GetComponent<StarbucksJiYeon>();
                    if (jiYeon != null)
                    {
                        SetFocus(jiYeon);
                    }
                }
                else
                {
                    RemoveFocus();
                }

                // Stop focusing any objects
            }
        }
	}
    void SetFocus(StarbucksJiYeon newFocus)
    {
        if(newFocus != focus)
        {
            if(focus != null)
            {
                focus.OnDefocused();
            }
            focus = newFocus;
            motor.FollowTarget(newFocus);
        }
        newFocus.OnFocused(transform);
    }
    void RemoveFocus()
    {
        if(focus != null)
        {
            focus.OnDefocused();
        }
        focus = null;
        motor.StopFollowingTarget();
    }
}
