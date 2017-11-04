using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Yarn.Unity.Example;
using Yarn.Unity;

public class DialogueSwitcher : MonoBehaviour {
    public Transform GameManager;
    DetroitGameManager gameManager;

    DetroitCameraController camController;

    public DialogueRunner dialogueRunner;
    DialogueUI dialogueUI;
    [System.Serializable]
    public struct DialogueContainer
    {
        public GameObject dialogueContainer;
        public TextMeshProUGUI text;
        public string name; 
    }

    public DialogueContainer[] dialogueContainers;

    private void Start()
    {
        dialogueUI = dialogueRunner.GetComponent<DialogueUI>();
        camController = Camera.main.GetComponent<DetroitCameraController>(); ;

    }

    [YarnCommand("setSpeakerName")]
    public void SetSpeakerName(string speakerName)
    {
        GameObject c = null;
        TextMeshProUGUI t = null;

        foreach (var container in dialogueContainers)
        {
            if (container.name == speakerName)
            {
                c = container.dialogueContainer;
                t = container.text;
                break;
            }
        }
        if (c == null)
        {
            Debug.LogErrorFormat("Can't find sprite named {0}!", speakerName);
            return;
        }
        Debug.Log("Speaker Name: " + speakerName);
        dialogueUI.DialogueContainer = c;
        dialogueUI.LineText = t;
        Debug.Log(dialogueUI.DialogueContainer.name);
    }
    [YarnCommand("beginQTE")]
    public void BeginQTE(string targetName)
    {
        gameManager = GameManager.GetComponent<DetroitGameManager>();
        Debug.Log("Target name: " + targetName);
        Transform Target = GameObject.Find(targetName).GetComponent<Transform>();
        gameManager.StartQTE();
    }

}

