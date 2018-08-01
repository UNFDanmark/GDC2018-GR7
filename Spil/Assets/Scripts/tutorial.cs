using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

/// <Credit>
/// Author: Julius 'Bukz' Bendt, Juto Studio
/// https://wwww.juto.dk
/// Free-for-all license
/// </Credit>
public class tutorial : MonoBehaviour {

    public TextMeshProUGUI player1, player2, button;
    public List<TutorialInfo> info = new List<TutorialInfo>();
    public UIEffects effects;

    private int index = -1; //NextTut will make it 0.

    Coroutine p1, p2;


    GameObject c_pp1, c_pp2;
    
    [System.Serializable]
    public struct TutorialInfo
    {
        public string player1Desc, player2Desc;
        public GameObject pp1, pp2;
        public float time;

    }


    public void StartTutorial()
    {
        index = -1;
        button.text = "Next";
        player1.text = "";
        player2.text = "";

        NextTut();
    }


    public void NextTut()
    {

        index++;


        if(index >= info.Count-1)
        {
            if(button.text != "Play!")
            {

                if (p1 != null)
                {
                    StopCoroutine(p1);
                    StopCoroutine(p2);
                }

                StartCoroutine(effects.SmoothTextAdd(button, "Play!", 4));
                p1 = StartCoroutine(effects.SmoothTextAdd(player1, info[index].player1Desc, info[index].time));
                p2 = StartCoroutine(effects.SmoothTextAdd(player2, info[index].player2Desc, info[index].time));
            }
            else
            {
                SceneManager.LoadScene("GameScene");
            }
            
        }
        else
        {

            if(p1 != null)
            {
                StopCoroutine(p1);
                StopCoroutine(p2);
            }

            if (c_pp1 != null)
                c_pp1.SetActive(false);
            if (c_pp2 != null)
                c_pp2.SetActive(false);


            info[index].pp1.SetActive(true);
            info[index].pp2.SetActive(true);

            c_pp1 = info[index].pp1;
            c_pp2 = info[index].pp2;

            p1 = StartCoroutine(effects.SmoothTextAdd(player1, info[index].player1Desc, info[index].time));
            p2 = StartCoroutine(effects.SmoothTextAdd(player2, info[index].player2Desc, info[index].time));


        }

    }
}
