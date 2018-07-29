using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleStoveManager : MonoBehaviour {
    public PlayerManager.PossibleItems itemInStove = PlayerManager.PossibleItems.empty;
    public float stoveTimer = 5f;
    public bool stoveDone = false;
    public TextMesh timerText;
    public GameObject itemObject;

    void Start()
    {
        timerText = GetComponentInChildren<TextMesh>();
    }

    // Update is called once per frame
    void Update () {

        if (itemInStove != PlayerManager.PossibleItems.empty)
        {
            stoveTimer -= Time.deltaTime;
            if (stoveTimer <= 0)
            {
                // Done
                stoveDone = true;
                if (stoveTimer <= -5)
                {
                    // Burned
                }
            }
        }
        else
        {
            stoveTimer = 5f;
            stoveDone = false;
        }
        
        if (stoveTimer > 0 && itemInStove != PlayerManager.PossibleItems.empty)
        {
            timerText.text = Mathf.RoundToInt(stoveTimer).ToString();
        }
        else if (stoveTimer < 0)
        {

            timerText.text = "Done";

        }
        else if (itemInStove == PlayerManager.PossibleItems.empty)
        {
            timerText.text = "Empty";
        }

    }
}
