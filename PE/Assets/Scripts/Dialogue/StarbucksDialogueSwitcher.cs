using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Yarn.Unity.Example;
using Yarn.Unity;

public class StarbucksDialogueSwitcher : MonoBehaviour {
 
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
    [YarnCommand("BeginKayakGameplay")]
    public void BeginKayakGameplay()
    {
        FindObjectOfType<Kayak>().BeginGameplay();
        Debug.Log("Begin Kayaking!");
    }
}

