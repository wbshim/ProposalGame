using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;
using Yarn.Unity.Example;

[RequireComponent(typeof(KayakController))]
public class Kayak : MonoBehaviour {
    public TouchInput swipeControls;
    public Transform player;
    public Transform PaddleLeft;
    public Transform PaddleRight;
    public Transform HandLeft;
    public Transform HandRight;

    private Vector3 direction;
    Quaternion deltaRotation;
    float targetDeltaAngularVelocity;
    float currentAngularVelocity;
    float relativeInputPosition;
    Animator animator;
    float strokeSide;
    KayakController kayak;

    bool beginGameplay;

    public Canvas UI;
    UIManager uiManager;

    AudioManager audioManager;

    public AudioSource RiverSound;

    private void Awake()
    {
        uiManager = UI.GetComponent<UIManager>();
    }
    private void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        RiverSound = audioManager.Play2("River",0.15f,1);
        animator = player.GetComponentInChildren<Animator>();
        kayak = player.GetComponent<KayakController>();
        PaddleRight.gameObject.SetActive(false);
        beginGameplay = false;
    }

	// Update is called once per frame
	private void Update ()
    {
        if(beginGameplay)
        {
            if (Input.GetMouseButton(0))
            {
                relativeInputPosition = Mathf.Sign(Input.mousePosition.x - Screen.width / 2);
            }

            if (swipeControls.SwipeUp)
            {
            }
            if (swipeControls.SwipeDown)
            {
                direction = player.transform.forward;
                if (relativeInputPosition < 0)
                {
                    animator.SetFloat("strokeSide", 0);
                    //PaddleLeft.transform.GetComponent<Renderer>().enabled = true;
                    //PaddleRight.transform.GetComponent<Renderer>().enabled = false;
                    PaddleLeft.gameObject.SetActive(true);
                    PaddleRight.gameObject.SetActive(false);
                }

                else
                {
                    animator.SetFloat("strokeSide", 1);
                    //PaddleLeft.transform.GetComponent<Renderer>().enabled = false;
                    //PaddleRight.transform.GetComponent<Renderer>().enabled = true;
                    PaddleLeft.gameObject.SetActive(false);
                    PaddleRight.gameObject.SetActive(true);
                }
                animator.SetTrigger("isStroking");
                kayak.moveKayak(direction, relativeInputPosition);
                audioManager.Play("Paddle");
            }
        }        
    }

    public void BeginDialogue()
    {
        FindObjectOfType<DialogueRunner>().StartDialogue("Start");
        Debug.Log("Begin GR Dialogue");
    }

    public void BeginGameplay()
    {
        
        beginGameplay = true;
        uiManager.ShowStartCard();
    }


    //IEnumerator moveKayak(Vector3 targetPosition)
    //{
    //    while(Vector3.Distance(player.transform.position,targetPosition) > 0.05f)
    //    {
    //        player.transform.position = Vector3.Lerp(player.transform.position, targetPosition, SmoothSpeed * Time.deltaTime);
    //        yield return null;
    //    }
    //}
}
