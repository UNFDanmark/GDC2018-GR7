using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour {
    
    public int foodPlateSpawnAmount;
    public int dirtyPlateSpawnAmount;
    public GameObject foodPlate;
    public GameObject dirtyPlate;
    public GameObject winManager;
    public Item emptyItem = new Item(PossibleItems.empty, ItemState.none, ItemPrefabDir.none);
    public int potTableFoodNeeded = 3;
    public int dirtyPlateCounter;
    public int orderAmount = 1;

    public AudioClip baggrundsLyd;

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
        AudioPlayer.playSound(baggrundsLyd, false, true);
    }
    public enum ItemState
    {

        raw,
        prept,
        done,
        burned,
        dirty,
        clean,
        none,
        finishedDirtyPlate1,
        finishedDirtyPlate2,
        finishedDirtyPlate3

    };

    public enum PossibleItems
    {
        empty,
        dirtyPlate,
        foodPlate,
        finished,
        finishedDirtyPlate
    };

    public enum ItemPrefabDir
    {
        none,
        Beef,
        Salami,
        Broccoli,
        Carrot,
        dirtyPlate,
        lessDirtyPlate,
        Pot
        // Add other food prefabs

    }

    public class Item
    {
        public PossibleItems possibleItems;
        public ItemState itemState;
        public ItemPrefabDir prefabDir;


        public Item(PossibleItems itemType, ItemState stateOfItem, ItemPrefabDir dirOfModelPrefab)
        {
            possibleItems = itemType;
            itemState = stateOfItem;
            prefabDir = dirOfModelPrefab;
        }
    }


    public void Update()
    {
        if (dirtyPlateCounter == 0 && SceneManager.GetActiveScene().name != "WinScene")
        {
            WinManagerScript.winnerRole = "Washer";
            SceneManager.LoadScene("WinScene");
        }
        else if (orderAmount == 0 && SceneManager.GetActiveScene().name != "WinScene")
        {
            WinManagerScript.winnerRole = "Cook";
            SceneManager.LoadScene("WinScene");
        }
    }

    public void RemoveOrder()
    {
        orderAmount--;
    }
}
