using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyManager : MonoBehaviour {


    private static string[] keys = { "q", "w", "e", "r", "t", "y", "u", "i", "o", "p", "f", "g", "h", "j", "k", "l", "z", "x", "c", "v", "b", "n", "m" };

    private static int p1Amount, p2Amount;
    private static string p1Key1, p1Key2;
    private static string p2Key1, p2Key2;

    public static void TriggerEnterKey(PlayerManager player,int amount)
    {



        string key1 = keys[Random.Range(0, keys.Length - 1)];
        string key2 = keys[Random.Range(0, keys.Length - 1)];

        player.UIkey.GetComponentInChildren<TextMeshProUGUI>().text = key1;
        player.UIkey2.GetComponentInChildren<TextMeshProUGUI>().text = key2;


        if (player.playerRole == PlayerManager.Role.cook)
        {
            p1Key1 = key1;
            p1Key2 = key2;
            p1Amount = amount;
        }
        else
        {
            p2Key1 = key1;
            p2Key2 = key2;
            p2Amount = amount;
        }
           

    }


    private void Update()
    {
       // if(Input.GetKey())
    }

}
