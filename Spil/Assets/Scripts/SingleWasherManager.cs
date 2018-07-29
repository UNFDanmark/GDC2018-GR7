using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleWasherManager : MonoBehaviour
{
    public PlayerManager.PossibleItems itemInWasher = PlayerManager.PossibleItems.empty;
    public float washerTimer = 5f; // Washer skal tage længere tid en stove
    public bool washerDone = false;
    public TextMesh timerText;

    public void Start()
    {
        timerText = GetComponentInChildren<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {

        if (itemInWasher != PlayerManager.PossibleItems.empty)
        {
            washerTimer -= Time.deltaTime;
            if (washerTimer <= 0)
            {
                washerDone = true;
            }
        }
        else
        {
            washerTimer = 5f;
            washerDone = false;
        }

        if (washerTimer > 0 && itemInWasher != PlayerManager.PossibleItems.empty)
        {
            timerText.text = Mathf.RoundToInt(washerTimer).ToString();
        }
        else if (washerTimer < 0)
        {

            timerText.text = "Done";

        }
        else if (itemInWasher == PlayerManager.PossibleItems.empty)
        {
            timerText.text = "Empty";
        }

    }
}
