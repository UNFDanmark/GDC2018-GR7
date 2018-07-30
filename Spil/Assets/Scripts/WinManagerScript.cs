using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WinManagerScript : MonoBehaviour {

    public TextMeshProUGUI winText;

    public static string winnerRole;

    private void Start()
    {
        winText.text = winnerRole + " Wins!";
    }
}
