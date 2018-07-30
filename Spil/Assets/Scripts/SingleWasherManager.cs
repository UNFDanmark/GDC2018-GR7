﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleWasherManager : MonoBehaviour
{
    public GameHandler.Item itemInWasher = new GameHandler.Item(GameHandler.PossibleItems.empty, GameHandler.ItemState.none, GameHandler.ItemPrefabDir.none);
    public int amountOfPlates;
    public int setWasherTime;
    private float washerTimer = 5f;
    public bool washerStarted = false;
    public bool washerDone = false;
    public bool washerEmptyMode = false;
    public TextMesh timerText;

    public void Start()
    {
        timerText = GetComponentInChildren<TextMesh>();
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
            washerTimer -= Time.deltaTime;
            if (washerTimer <= 0)
            {
                itemInWasher.itemState = GameHandler.ItemState.clean;
                washerStarted = false;
                washerEmptyMode = true;
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
            timerText.text = "Ready to start washer";
        }

    }

    public void StartWasher()
    {
        washerStarted = true;
    }
}
