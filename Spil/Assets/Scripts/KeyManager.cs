using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


/// <summary>
/// Creates a GameObject with a audiosource, which will be played.
///
/// CREDIT:
/// Julius 'Bukz' Bendt
/// Juto Studio
/// https://wwww.juto.dk
/// Free-for-all license
///
/// </summary>

public class KeyManager : MonoBehaviour {


    private static string[] keys = { "q", "e", "r", "t", "y", "u", "i", "o", "p", "f", "g", "h", "j", "k", "l", "z", "x", "c", "v", "b", "n", "m" };

    private static int p1Amount, p2Amount;
    private static string p1Key1, p1Key2;
    private static string p2Key1, p2Key2;
    private static bool p1key1turn, p2key1turn;

    private static UIManager ui;

    private static RectTransform p1r, p1l, p2r, p2l;
    private static CanvasGroup p1cg, p2cg;

    public PlayerManager player1,player2;

    private static OnQuickEventOver p1event,p2event;

    public delegate void OnQuickEventOver();



    private void Start()
    {
        ui = FindObjectOfType<UIManager>();

        p1r = player1.UIkey.GetComponent<RectTransform>();
        p1l = player1.UIkey2.GetComponent<RectTransform>();
        p2r = player2.UIkey.GetComponent<RectTransform>();
        p2l = player2.UIkey2.GetComponent<RectTransform>();

        p1cg = p1r.GetComponentInParent<CanvasGroup>();
        p2cg = p2r.GetComponentInParent<CanvasGroup>();
    }

    public static void TriggerQuickEvent(PlayerManager player,int amount,OnQuickEventOver _event)
    {



        string key1 = keys[Random.Range(0, keys.Length - 1)];
        string key2 = keys[Random.Range(0, keys.Length - 1)];

        while(key1 == key2)
        {
            key2 = keys[Random.Range(0, keys.Length - 1)];
        }

        player.UIkey.GetComponentInChildren<TextMeshProUGUI>().text = key1;
        player.UIkey2.GetComponentInChildren<TextMeshProUGUI>().text = key2;


        if (player.playerRole == PlayerManager.Role.cook)
        {
            p1Key1 = key1;
            p1Key2 = key2;

            p1Amount = amount;

            p1cg.alpha = 1;

            ui.KeyPressed(p1r, p1l);

            p1event = _event;
        }
        else
        {
            p2Key1 = key1;
            p2Key2 = key2;

            p2Amount = amount;

            p2cg.alpha = 1;

            ui.KeyPressed(p2r, p2l);

            p2event = _event;
        }
           

    }


    private void Update()
    {
        if(!string.IsNullOrEmpty(p1Key1))
        {
            if (Input.GetKeyDown(p1Key1) && !p1key1turn && p1Amount > 0)
            {
                ui.KeyPressed(p1l,p1r);
                p1Amount--;
                p1key1turn = true;
            }

            if (Input.GetKeyDown(p1Key2) && p1key1turn && p1Amount > 0)
            {
                ui.KeyPressed(p1r, p1l);
                p1Amount--;
                p1key1turn = false;
            }
        }

        if (!string.IsNullOrEmpty(p2Key1))
        {
            if (Input.GetKeyDown(p2Key1) && !p2key1turn && p2Amount > 0)
            {
                ui.KeyPressed(p2l, p2r);
                p2Amount--;
                p2key1turn = true;
            }

            if (Input.GetKeyDown(p2Key2) && p2key1turn && p2Amount > 0)
            {
                ui.KeyPressed(p2r, p2l);
                p2Amount--;
                p2key1turn = false;
            }
        }

        if(p1Amount == 0)
        {
            p1cg.alpha = 0;

            if(p1event != null)
            {
                p1event.Invoke();
                p1event = null;
            }
        }

        if (p2Amount == 0)
        {
            p2cg.alpha = 0;

            if (p2event != null)
            {
                p2event.Invoke();
                p2event = null;
            }
        }

    }

}
