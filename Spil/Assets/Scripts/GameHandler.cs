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
    public Item emptyItem = new Item(PossibleItems.empty, ItemState.none);
    public int potTableFoodNeeded = 3;
    public int dirtyPlateCounter;
    public int orderAmount = 3;

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public enum ItemState
    {

        raw,
        prept,
        done,
        burned,
        dirty,
        clean,
        none

    };

    public enum PossibleItems
    {
        empty,
        dirtyPlate,
        foodPlate,
        finished
    };

    public class Item
    {
        public PossibleItems possibleItems;
        public ItemState itemState;

        public Item(PossibleItems itemType, ItemState stateOfItem)
        {
            possibleItems = itemType;
            itemState = stateOfItem;
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
