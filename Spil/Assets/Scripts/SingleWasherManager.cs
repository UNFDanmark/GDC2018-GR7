using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleWasherManager : MonoBehaviour
{
    public GameHandler.Item itemInWasher = new GameHandler.Item(GameHandler.PossibleItems.empty, GameHandler.ItemState.none);
    public float washerTimer = 6f; // Washer skal tage længere tid en stove
    public bool washerDone = false;
    public TextMesh timerText;

    public void Start()
    {
        timerText = GetComponentInChildren<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {

        if (itemInWasher.possibleItems != GameHandler.PossibleItems.empty)
        {
            washerTimer -= Time.deltaTime;
            if (washerTimer <= 0)
            {
                itemInWasher.itemState = GameHandler.ItemState.clean;
                washerDone = true;
            }
        }
        else
        {
            washerTimer = 6f;
            washerDone = false;
        }

        if (washerTimer > 0 && itemInWasher.possibleItems != GameHandler.PossibleItems.empty)
        {
            timerText.text = Mathf.RoundToInt(washerTimer).ToString();
        }
        else if (washerTimer < 0)
        {

            timerText.text = "Done";

        }
        else if (itemInWasher.possibleItems == GameHandler.PossibleItems.empty)
        {
            timerText.text = "Empty";
        }

    }
}
