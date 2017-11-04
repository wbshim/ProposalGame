using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class MenuDialogue : MonoBehaviour {

    MainMenuGameManager gameManager;
    AppleJiYeon JiYeon;
    public string[] Dialogues;
    public Transform CenterTarget;

    int outsideTargetIndex = 1;
    int dialoguesIndex;

    bool firstMove = true;

    private void Awake()
    {
        gameManager = FindObjectOfType<MainMenuGameManager>();
        dialoguesIndex = Dialogues.Length;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "JiYeon")
        {
            JiYeon = other.GetComponent<AppleJiYeon>();
            switch (name)
            {
                case "Left":
                    if(!firstMove)
                        StartCoroutine(MoveToCenter());
                    break;
                case "Center":
                    Debug.Log(other.tag + " entered " + name);
                    JiYeon.RotateTowardsTarget(gameManager.Wonbo);
                    FindObjectOfType<DialogueRunner>().StartDialogue(Dialogues[dialoguesIndex % Dialogues.Length]);
                    gameManager.StopKeyboard();
                    dialoguesIndex++;
                    break;
                case "Right":
                    StartCoroutine(MoveToCenter());
                    break;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        firstMove = false;
        Debug.Log("Moved out, firstMove = " + firstMove);
    }
    [YarnCommand("MainMenuMove")]
    public void MainMenuMove()
    {
        JiYeon.MoveToPoint(gameManager.Waypoints[outsideTargetIndex % 2].position);
        outsideTargetIndex++;
        gameManager.PlayKeyboard();
    }
    IEnumerator MoveToCenter()
    {
        yield return new WaitForSeconds(1);
        JiYeon.MoveToPoint(CenterTarget.position);
    }

}
