using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleStoveManager : MonoBehaviour {
    public GameHandler.Item itemInStove = new GameHandler.Item(GameHandler.PossibleItems.empty, GameHandler.ItemState.none, GameHandler.ItemPrefabDir.none);
    public GameObject audioDatabase;
    public float stoveTimer = 15f;
    public float burnedTimer = 10f;
    public bool stoveDone = false;
    public TextMesh timerText;
    public bool stovePaused;
    public bool playingSound = false;

    void Start()
    {
        timerText = GetComponentInChildren<TextMesh>();
        stovePaused = true;
    }

    // Update is called once per frame
    void Update () {

        int burnedTimerInt = Mathf.RoundToInt(burnedTimer);

        if (itemInStove.possibleItems != GameHandler.PossibleItems.empty)
        {
            if (stovePaused == false)
            {
                if (!playingSound)
                {
                    AudioPlayer.playSound(audioDatabase.GetComponent<AudioDatabase>().stoveSound, true, true);
                    playingSound = true;
                }
                stoveTimer -= Time.deltaTime;
            }
            if (stoveTimer <= 0)
            {
                // Done
                itemInStove.itemState = GameHandler.ItemState.done;
                stoveDone = true;
                if (stoveTimer <= -10)
                {
                    // Burned
                    itemInStove.itemState = GameHandler.ItemState.burned;
                }
            }
        }
        else
        {
            GameObject.Destroy(GameObject.Find("AudioPlayer " + audioDatabase.GetComponent<AudioDatabase>().stoveSound.name));
            playingSound = false;
            stoveTimer = 15f;
            burnedTimer = 10f;
            stoveDone = false;
            stovePaused = true;
        }

        if (stoveTimer > 0 && itemInStove.possibleItems != GameHandler.PossibleItems.empty)
        {
            timerText.text = Mathf.RoundToInt(stoveTimer).ToString();
        }
        else if (stoveTimer < 0 && stoveTimer > -10 && itemInStove.itemState != GameHandler.ItemState.burned)
        {
            itemInStove.itemState = GameHandler.ItemState.done;
            burnedTimer -= Time.deltaTime;
        }
        if (burnedTimer < 0)
        {
            timerText.text = "Burned";
            itemInStove.itemState = GameHandler.ItemState.burned;
        }
        if (itemInStove.itemState == GameHandler.ItemState.done)
        {
            timerText.text = "Done\nTime until burned: " + burnedTimerInt;
        }
        else if (itemInStove.possibleItems == GameHandler.PossibleItems.empty)
        {
            timerText.text = "Empty";
        }

    }
}
