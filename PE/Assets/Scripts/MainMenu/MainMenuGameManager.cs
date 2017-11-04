using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuGameManager : MonoBehaviour {

    AudioManager audioManager;
    AudioSource keyboard;
    
    MainMenuCard card;
    AppleJiYeon JiYeon;
    public Transform Wonbo;
    public Transform[] Waypoints;
    public Transform CenterPoint;
    int dialogueCount;
    int dialogueIndex;

    // UI
    MainMenuCard menuCard;

    public string NextLevel;

    private void Awake()
    {
        menuCard = FindObjectOfType<MainMenuCard>();
        audioManager = FindObjectOfType<AudioManager>();
        card = FindObjectOfType<MainMenuCard>();
        JiYeon = FindObjectOfType<AppleJiYeon>();
        dialogueCount = Waypoints.Length;
        dialogueIndex = 3;
    }
    // Use this for initialization
    void Start () {
        card.ShowMenu();
        StartCoroutine(FirstMove());
        keyboard = audioManager.GetSource("Keyboard");
        PlayKeyboard();
        audioManager.SetBackgroundMusic("BackgroundRose");
    }
    public void PlayKeyboard()
    {
        keyboard.Play();
    }
    public void StopKeyboard()
    {
        keyboard.Stop();
    }
	IEnumerator FirstMove()
    {
        yield return new WaitForSeconds(3);
        JiYeon.MoveToPoint(CenterPoint.position);
    }
    public void LoadNextLevel()
    {
        StartCoroutine(_LoadNextLevel(2));
    }
    IEnumerator _LoadNextLevel(float t)
    {
        Debug.Log("Next level");
        audioManager.Play("Win", 0.25f);
        menuCard.CloseMenu();
        audioManager.SetBackgroundMusicVolume(0);
        keyboard.volume = 0;
        yield return new WaitForSeconds(t);
        SceneManager.LoadScene(NextLevel, LoadSceneMode.Single);
    }
}
