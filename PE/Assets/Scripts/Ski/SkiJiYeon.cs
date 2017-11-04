using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkiJiYeon : MonoBehaviour {

    Animator anim;
    SkiGameManager gameManager;

    private void Start()
    {
        anim = GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager").GetComponent<SkiGameManager>();
        StartCoroutine(GoAnimation());
    }

    public IEnumerator GoAnimation()
    {
        float s = 0;
        while(s<1)
        {
            anim.SetFloat("Speed", s);
            s+=0.05f;
            yield return null;
        }
    }

    public void AnimateTurnAngle(float a)
    {
        anim.SetFloat("Angle", a);
    }
}
