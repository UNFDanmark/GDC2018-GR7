using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleWasherManager : MonoBehaviour
{
    public GameHandler.Item itemInWasher = new GameHandler.Item(GameHandler.PossibleItems.empty, GameHandler.ItemState.none, GameHandler.ItemPrefabDir.none);
    public int amountOfPlates;
    public int setWasherTime;
    private float washerTimer = 20f;
    public bool washerStarted = false;
    public bool washerDone = false;
    public bool washerEmptyMode = false;
    public bool washerPaused;
    public TextMesh timerText;

    public void Start()
    {
        timerText = GetComponentInChildren<TextMesh>();
        washerPaused = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (amountOfPlates >= 3 && washerStarted == false)
        {
            StartWasher();
        }
        
        if (washerEmptyMode == true && amountOfPlates <= 0)
        {
            washerEmptyMode = false;
        }



        if (washerStarted)
        {
            if(washerPaused == false)
            {
                washerTimer -= Time.deltaTime;
            }

            if (washerTimer <= 0)
            {
                itemInWasher.itemState = GameHandler.ItemState.clean;
                washerStarted = false;
                washerEmptyMode = true;
                washerPaused = false;
            }
        }
        else
        {
            washerTimer = setWasherTime;
        }

        if (washerTimer > 0 && itemInWasher.possibleItems != GameHandler.PossibleItems.empty && !washerEmptyMode && washerStarted)
        {
            timerText.text = Mathf.RoundToInt(washerTimer).ToString();
        }
        else if (washerEmptyMode)
        {
            timerText.text = "Done.  " + amountOfPlates.ToString() + " left in the washer";
        }
        else if (itemInWasher.possibleItems == GameHandler.PossibleItems.empty && !washerEmptyMode)
        {
            timerText.text = "Empty";
        }
        else if (washerTimer > 0 && itemInWasher.possibleItems != GameHandler.PossibleItems.empty && !washerEmptyMode)
        {
            timerText.text = "Press to start";
        }

    }

    public void StartWasher()
    {
        washerStarted = true;
        
    }
}
