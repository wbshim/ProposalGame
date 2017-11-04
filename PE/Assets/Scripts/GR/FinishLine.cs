using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour {
    AudioManager audioManager;
    private void OnTriggerEnter(Collider other)
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        Debug.Log(other.tag + " crossed the finish line");
        StartCoroutine(ShowFinalCards());
    }
    IEnumerator ShowFinalCards()
    {
        UIManager uiManager = FindObjectOfType<UIManager>();
        uiManager.ShowFinishCard();
        audioManager.Play("Win", 0.25f);
        yield return new WaitForSeconds(3);
        Kayak kayak = FindObjectOfType<Kayak>();
        audioManager.Stop(kayak.RiverSound,2f);
        audioManager.SetBackgroundMusicVolume(0.025f);
        uiManager.ShowFinalCard();
    }
}
