using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class WinManagerScript : MonoBehaviour {

    public TextMeshProUGUI winText, other;

    public static string winnerRole;

    public UIEffects effect;

    public Color standardColor;

    const float LERPTIME = 1;

    private void Start()
    {
        winText.text = winnerRole + " Wins!";

        StartCoroutine(effect.TextFade(other, Color.clear, LERPTIME, standartToClear));

        FindObjectOfType<GameHandler>().ResetObj();
    }


    void standartToClear()
    {
        StartCoroutine(effect.TextFade(other, standardColor, LERPTIME, clearToStandart));
    }


    void clearToStandart()
    {
        StartCoroutine(effect.TextFade(other, Color.clear, LERPTIME, standartToClear));
    }

    private void Update()
    {
        if(Input.anyKeyDown)
        {
            GameObject.Destroy(GameObject.Find("GameManager"));
            SceneManager.LoadScene("GameScene");
        }
    }
}
