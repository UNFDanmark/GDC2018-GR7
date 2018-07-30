using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public Role playerRole = Role.cook;
    public GameObject key;
    public GameObject CurrentStand;
    public GameObject itemPrefab;
    public GameObject holdingItem;
    public GameObject gameHandlerObject;
    public GameObject tableItemSpawner;
    public GameHandler.Item CurrentItem;
    public bool onTile;
    public bool canMove = true;
    public bool quickEventActive = false;
    public Transform itemPlacement;
    public GameHandler.Item emptyItem = new GameHandler.Item(GameHandler.PossibleItems.empty, GameHandler.ItemState.none, GameHandler.ItemPrefabDir.none);
    public GameObject pot;
    public GameObject currentOrder;

    public string horizontal = "p1H", vertical = "p1V", action = "p1A";

    public GameObject UIkey, UIkey2;

    public enum Role
    {
       cook,
       cleaner
    }

    private void Awake()
    {
        CurrentItem = emptyItem;
    }

    // Update is called once per frame
    void Update () {

        if (currentOrder == null)
        {
            currentOrder = GameObject.FindGameObjectWithTag("Order");
        }

        key.SetActive(onTile);
        TileChecker();
        if (onTile)
        {

            if (Input.GetButtonDown(action))
            {
                Actions();
            }

        }

	}

    void GrabItem()
    {
        holdingItem = Instantiate(itemPrefab, itemPlacement.position, Quaternion.identity, transform);
    }

    void TileChecker()
    {

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
        {

            if(hit.collider.gameObject.tag == "Tile")
            {
                //Player er på en tile
                onTile = true;
                CurrentStand = hit.collider.transform.parent.gameObject;

            }
            else
            {
                //Ikke på en tile
                onTile = false;
                CurrentStand = null;
            }
        }

    }

    public void Actions()
    {

        if (onTile)
        {
            if (CurrentStand.tag == "Table")
            {
                if (CurrentStand.GetComponent<SingleTableManager>().objectOnTable.possibleItems != GameHandler.PossibleItems.empty && CurrentItem.possibleItems == GameHandler.PossibleItems.empty)
                {
                    CurrentItem = CurrentStand.GetComponent<SingleTableManager>().objectOnTable;

                    itemPrefab = Resources.Load<GameObject>("Prefabs/" + CurrentItem.prefabDir.ToString());

                    CurrentStand.GetComponent<SingleTableManager>().objectOnTable = emptyItem;
                    GrabItem();

                }
                else if (CurrentStand.GetComponent<SingleTableManager>().objectOnTable.possibleItems == GameHandler.PossibleItems.empty && CurrentItem.possibleItems != GameHandler.PossibleItems.empty)
                {
                    //Place holded item on table
                    GameObject.Destroy(holdingItem);
                    CurrentStand.GetComponent<SingleTableManager>().objectOnTable = CurrentItem;
                    CurrentStand.GetComponent<SingleTableManager>().emptyTable = true;
                    CurrentItem = emptyItem;

                }
            }
            else if (CurrentStand.tag == "Stove")
            {
                //Gør noget med stove
                if (CurrentItem.possibleItems == GameHandler.PossibleItems.foodPlate)
                {
                    if (CurrentStand.GetComponent<SingleStoveManager>().itemInStove.possibleItems == GameHandler.PossibleItems.empty && CurrentItem.itemState == GameHandler.ItemState.prept)
                    {
                        
                        CurrentStand.GetComponent<SingleStoveManager>().itemInStove = CurrentItem;
                        CurrentItem = emptyItem;

                        //Kun grafisk ændring
                        GameObject.Destroy(holdingItem);

                    }
                }
                else if (CurrentItem.possibleItems == GameHandler.PossibleItems.empty && CurrentStand.GetComponent<SingleStoveManager>().itemInStove.possibleItems == GameHandler.PossibleItems.foodPlate && CurrentStand.GetComponent<SingleStoveManager>().stoveDone)
                {
                    //Tag noget fra stove 
                    CurrentItem = CurrentStand.GetComponent<SingleStoveManager>().itemInStove;
                    CurrentStand.GetComponent<SingleStoveManager>().itemInStove = emptyItem;

                    //Grafisk del
                    itemPrefab = Resources.Load<GameObject>("Prefabs/" + CurrentItem.prefabDir.ToString());
                    GrabItem();
                }

            }
            else if (CurrentStand.tag == "Washer")
            {
                //Gør noget med washer
                if (CurrentItem.possibleItems == GameHandler.PossibleItems.dirtyPlate)
                {
                    if (CurrentStand.GetComponent<SingleWasherManager>().amountOfPlates <= 3 && CurrentItem.itemState == GameHandler.ItemState.prept && CurrentStand.GetComponent<SingleWasherManager>().washerEmptyMode == false)
                    {
                        CurrentStand.GetComponent<SingleWasherManager>().itemInWasher = CurrentItem;
                        CurrentStand.GetComponent<SingleWasherManager>().amountOfPlates++;
                        CurrentItem = emptyItem;

                        //Kun grafisk ændring
                        GameObject.Destroy(holdingItem);

                    }
                }
                else if (CurrentItem.possibleItems == GameHandler.PossibleItems.empty || CurrentItem.possibleItems == GameHandler.PossibleItems.finishedDirtyPlate)
                {
                    //Start maskinen
                    if(CurrentStand.GetComponent<SingleWasherManager>().washerEmptyMode == false)
                    {
                        if (CurrentStand.GetComponent<SingleWasherManager>().itemInWasher.possibleItems != GameHandler.PossibleItems.empty)
                        {
                            CurrentStand.GetComponent<SingleWasherManager>().StartWasher();
                        }
                    }

                    //Tag tallerken fra maskine
                    if(CurrentStand.GetComponent<SingleWasherManager>().washerEmptyMode)
                    {
                        CurrentStand.GetComponent<SingleWasherManager>().amountOfPlates--;
                        AddCleanPlateToStack();

                        if (CurrentStand.GetComponent<SingleWasherManager>().amountOfPlates <= 0)
                        {
                            CurrentStand.GetComponent<SingleWasherManager>().itemInWasher = emptyItem;
                        }
                    }
                }

            }
            else if (CurrentStand.tag == "CuttingBoard")
            {
               if (CurrentItem.possibleItems == GameHandler.PossibleItems.foodPlate && playerRole == Role.cook)
                {
                    if (!quickEventActive)
                    {
                        canMove = false;
                        quickEventActive = true;
                        KeyManager.TriggerQuickEvent(this, 30, QuickEventOver);
                    }
                }
            }
            else if (CurrentStand.tag == "Sink")
            {
                if (CurrentItem.possibleItems == GameHandler.PossibleItems.dirtyPlate && playerRole == Role.cleaner)
                {
                    if (!quickEventActive)
                    {
                        canMove = false;
                        quickEventActive = true;
                        KeyManager.TriggerQuickEvent(this, 30, QuickEventOver);
                    }
                }
            }
            else if (CurrentStand.tag == "PotTable")
            {
                if (CurrentItem.possibleItems == GameHandler.PossibleItems.foodPlate && CurrentItem.itemState == GameHandler.ItemState.done && playerRole == Role.cook)
                {
                    GameObject.Destroy(holdingItem);
                    gameHandlerObject.GetComponent<GameHandler>().potTableFoodNeeded--;
                    CurrentItem = emptyItem;
                    KeyManager.TriggerQuickEvent(this, 20, StewEventOver);
                    canMove = false;

                    if(gameHandlerObject.GetComponent<GameHandler>().potTableFoodNeeded == 0)
                    {
                    // Der er 3 ting i gryden, og den skal nu kunne samles op.
                    pot.SetActive(false);
                        CurrentItem = new GameHandler.Item(GameHandler.PossibleItems.finished, GameHandler.ItemState.none, GameHandler.ItemPrefabDir.Pot);
                        itemPrefab = Resources.Load<GameObject>("Prefabs/" + CurrentItem.prefabDir.ToString());
                        GrabItem();
                    }
                }
            }
            else if (CurrentStand.tag == "CookFinish")
            {
                if (CurrentItem.possibleItems == GameHandler.PossibleItems.finished && playerRole == Role.cook)
                {
                    GameObject.Destroy(holdingItem);
                    pot.SetActive(true);
                    CurrentItem = new GameHandler.Item(GameHandler.PossibleItems.dirtyPlate, GameHandler.ItemState.dirty, GameHandler.ItemPrefabDir.dirtyPlate);
                    itemPrefab = Resources.Load<GameObject>("Prefabs/" + CurrentItem.prefabDir.ToString());
                    GameObject.Destroy(currentOrder);
                    gameHandlerObject.GetComponent<GameHandler>().dirtyPlateCounter++;
                    TableItemSpawner.spawnedFoodPlates = 0;
                    gameHandlerObject.GetComponent<GameHandler>().foodPlateSpawnAmount++;
                    tableItemSpawner.GetComponent<TableItemSpawner>().SpawnFoodPlate();

                    GrabItem();
                    gameHandlerObject.GetComponent<GameHandler>().RemoveOrder();

                }
            }
            else if (CurrentStand.tag == "WasherFinish")
            {
                if (CurrentItem.possibleItems == GameHandler.PossibleItems.finishedDirtyPlate && playerRole == Role.cleaner)
                {
                    if(CurrentItem.itemState == GameHandler.ItemState.finishedDirtyPlate1)
                    {
                        GameObject.Destroy(holdingItem);
                        CurrentItem = emptyItem;
                        gameHandlerObject.GetComponent<GameHandler>().dirtyPlateCounter -= 1;
                    }

                    if (CurrentItem.itemState == GameHandler.ItemState.finishedDirtyPlate2)
                    {
                        GameObject.Destroy(holdingItem);
                        CurrentItem = emptyItem;
                        gameHandlerObject.GetComponent<GameHandler>().dirtyPlateCounter -= 2;
                    }

                    if (CurrentItem.itemState == GameHandler.ItemState.finishedDirtyPlate3)
                    {
                        GameObject.Destroy(holdingItem);
                        CurrentItem = emptyItem;
                        gameHandlerObject.GetComponent<GameHandler>().dirtyPlateCounter -= 3;
                    }

                }
            }
            else if (CurrentStand.tag == "GarbageBin")
            {
                if (CurrentItem.itemState == GameHandler.ItemState.burned)
                {
                    CurrentItem = emptyItem;
                    GameObject.Destroy(holdingItem);
                    TableItemSpawner.spawnedFoodPlates--;
                    tableItemSpawner.GetComponent<TableItemSpawner>().SpawnFoodPlate();
                }
            }
        }
    }

    public void QuickEventOver()
    {
        canMove = true;
        quickEventActive = false;

        // Rimeligt hardcoded. Ændrer dette hvis at vi bruger quicktime til andre ting end at washe og cutte
        CurrentItem.itemState = GameHandler.ItemState.prept;

    }

    public void StewEventOver()
    {
        canMove = true;
        quickEventActive = false;
    }

    public void AddCleanPlateToStack()
    {
        if (CurrentItem.possibleItems != GameHandler.PossibleItems.finishedDirtyPlate)
        {
            CurrentItem = new GameHandler.Item(GameHandler.PossibleItems.finishedDirtyPlate, GameHandler.ItemState.finishedDirtyPlate1, GameHandler.ItemPrefabDir.dirtyPlate);
            //Grafisk del
            itemPrefab = Resources.Load<GameObject>("Prefabs/finishedDirtyPlate1");
            GrabItem();
        }
        else
        {
            switch (CurrentItem.itemState)
            {

                case GameHandler.ItemState.finishedDirtyPlate1:
                    CurrentItem.itemState = GameHandler.ItemState.finishedDirtyPlate2;
                    GameObject.Destroy(holdingItem);
                    itemPrefab = Resources.Load<GameObject>("Prefabs/finishedDirtyPlate2");
                    GrabItem();
                    break;
                case GameHandler.ItemState.finishedDirtyPlate2:
                    CurrentItem.itemState = GameHandler.ItemState.finishedDirtyPlate3;
                    GameObject.Destroy(holdingItem);
                    itemPrefab = Resources.Load<GameObject>("Prefabs/finishedDirtyPlate3");
                    GrabItem();
                    break;
            }
        }
    }
}
