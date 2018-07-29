using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleStoveManager : MonoBehaviour {
    public GameHandler.Item itemInStove = new GameHandler.Item(GameHandler.PossibleItems.empty, GameHandler.ItemState.none);
    public float stoveTimer = 5f;
    public bool stoveDone = false;
    public TextMesh timerText;

    void Start()
    {
        timerText = GetComponentInChildren<TextMesh>();
    }

    // Update is called once per frame
    void Update () {

        if (itemInStove.possibleItems != GameHandler.PossibleItems.empty)
        {
            stoveTimer -= Time.deltaTime;
            if (stoveTimer <= 0)
            {
                // Done
                itemInStove.itemState = GameHandler.ItemState.done;
                stoveDone = true;
                if (stoveTimer <= -5)
                {
                    // Burned
                    itemInStove.itemState = GameHandler.ItemState.burned;
                }
            }
        }
        else
        {
            stoveTimer = 5f;
            stoveDone = false;
        }
        
        if (stoveTimer > 0 && itemInStove.possibleItems != GameHandler.PossibleItems.empty)
        {
            timerText.text = Mathf.RoundToInt(stoveTimer).ToString();
        }
        else if (stoveTimer < 0)
        {

            timerText.text = "Done";

        }
        else if (itemInStove.possibleItems == GameHandler.PossibleItems.empty)
        {
            timerText.text = "Empty";
        }

    }
}
