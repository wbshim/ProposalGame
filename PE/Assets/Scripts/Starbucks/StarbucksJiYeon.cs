using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Yarn.Unity;

public class StarbucksJiYeon : MonoBehaviour {


    Animator jiyeonAnimator;
    public float radius = 2f;
    public Transform InteractionTransform;

    bool isFocus = false;
    Transform player;
    public DialogueFollow WonboDialogue;
    public Transform WonboSitting;
    bool hasInteracted = false;
    // Use this for initialization
    void Start()
    {
        jiyeonAnimator = GetComponentInChildren<Animator>();
    }
    //public virtual void Interact()
    //{

    //}
    // Update is called once per frame
    void Update () {
		if(isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, InteractionTransform.position);
            if(distance <= radius)
            {
                Debug.Log("Interact!");
                Destroy(player.gameObject);
                hasInteracted = true;
                jiyeonAnimator.SetFloat("SittingPosition", 1);
                player.gameObject.SetActive(false);
                WonboDialogue.target = WonboSitting;
                WonboSitting.gameObject.SetActive(true);
                StartCoroutine(StartSecondDialogue());
            }
        }
	}
    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }
    public void OnDefocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(InteractionTransform.position, radius);
    }
    IEnumerator StartSecondDialogue()
    {
        yield return new WaitForSeconds(1f);
        FindObjectOfType<DialogueRunner>().StartDialogue("Start");
    }
}
