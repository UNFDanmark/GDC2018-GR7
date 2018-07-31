using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

/// <Credit>
/// Author: Julius 'Bukz' Bendt, Juto Studio
/// https://wwww.juto.dk
/// Free-for-all license
/// </Credit>

public class StartMenuBehavior : MonoBehaviour {


    public RectTransform play, credit, exit;
    public TextMeshProUGUI title;
    public UIEffects effects;


    public Image fader;
    public Color standardColor;

    public GameObject creditTab,tutorialTab;
    public RectTransform creditText;
    public float creditSpeed;

    const float MOVETIME = 0.75f; // how long the MoveRect should take, in sec

    public void Start()
    {
        StartCoroutine(effects.TextFade(title,standardColor, 0.3f));
        StartCoroutine(effects.MoveRect(play, new Vector3(-161,-133, 0), MOVETIME-0.2f));
        StartCoroutine(effects.MoveRect(credit, new Vector3(161, -133, 0), MOVETIME));
        StartCoroutine(effects.MoveRect(exit, new Vector3(0, -221, 0), MOVETIME+0.2f));
    }

 

    public void Play()
    {
        StartCoroutine(effects.ImageFade(fader, standardColor, 0.5f, PlayFaderFinish));
    }

    public void Credit()
    {
        StartCoroutine(effects.ImageFade(fader, standardColor, 1f, OnCreditInFinish));
    }

    public void Exit()
    {
        Application.Quit();
    }

    void PlayFaderFinish()
    {
        tutorialTab.SetActive(true);
        StartCoroutine(effects.ImageFade(fader, Color.clear, 0.5f, LoadedTut));
    }

    void LoadedTut()
    {
        FindObjectOfType<tutorial>().StartTutorial();
    }

    void OnCreditInFinish()
    {
        StartCoroutine(effects.ImageFade(fader, Color.clear, 2f));
        creditTab.SetActive(true);
        creditText.localPosition = new Vector3(0, 0, 0);
    }

    private void Update()
    {
        if(creditTab.activeInHierarchy)
        {
            creditText.localPosition = new Vector3(0, creditText.localPosition.y + creditSpeed, 0);
        }
    }

    public void CloseCredits()
    {
        creditTab.SetActive(false);
    }
}
