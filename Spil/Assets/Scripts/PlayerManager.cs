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
    public GameHandler.Item CurrentItem;
    public bool onTile;
    public bool canMove = true;
    public bool quickEventActive = false;
    public Transform itemPlacement;
    public GameHandler.Item emptyItem = new GameHandler.Item(GameHandler.PossibleItems.empty, GameHandler.ItemState.none);
    public GameObject pot;
    public GameObject currentOrder;

    public string horizontal = "p1H", vertical = "p1V", action = "p1A";

    public GameObject UIkey, UIkey2;

    TableItemSpawner tableItemSpawner;

    public enum Role
    {
       cook,
       cleaner
    }

    private void Awake()
    {
        CurrentItem = new GameHandler.Item(GameHandler.PossibleItems.empty, GameHandler.ItemState.none);
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

                    switch (CurrentStand.GetComponent<SingleTableManager>().objectOnTable.possibleItems)
                    {

                        case GameHandler.PossibleItems.dirtyPlate:
                            itemPrefab = Resources.Load<GameObject>("Prefabs/dirtyPlate");
                            break;
                        case GameHandler.PossibleItems.foodPlate:
                            itemPrefab = Resources.Load<GameObject>("Prefabs/foodPlate");
                            break;

                    }
                    CurrentItem = CurrentStand.GetComponent<SingleTableManager>().objectOnTable;
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
                    itemPrefab = Resources.Load<GameObject>("Prefabs/foodPlate");
                    GrabItem();
                }

            }
            else if (CurrentStand.tag == "Washer")
            {
                //Gør noget med washer
                if (CurrentItem.possibleItems == GameHandler.PossibleItems.dirtyPlate)
                {
                    if (CurrentStand.GetComponent<SingleWasherManager>().itemInWasher.possibleItems == GameHandler.PossibleItems.empty && CurrentItem.itemState == GameHandler.ItemState.prept)
                    {

                        CurrentStand.GetComponent<SingleWasherManager>().itemInWasher = CurrentItem;
                        CurrentItem = emptyItem;

                        //Kun grafisk ændring
                        GameObject.Destroy(holdingItem);

                    }
                }
                else if (CurrentItem.possibleItems == GameHandler.PossibleItems.empty && CurrentStand.GetComponent<SingleWasherManager>().itemInWasher.possibleItems == GameHandler.PossibleItems.dirtyPlate && CurrentStand.GetComponent<SingleWasherManager>().washerDone)
                {
                    //Tager item fra stove
                    CurrentItem = CurrentStand.GetComponent<SingleWasherManager>().itemInWasher;
                    CurrentStand.GetComponent<SingleWasherManager>().itemInWasher = emptyItem;

                    //Grafisk del
                    itemPrefab = Resources.Load<GameObject>("Prefabs/dirtyPlate");
                    GrabItem();
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

                    //if(gameHandlerObject.GetComponent<GameHandler>().potTableFoodNeeded == 0)
                    //{
                    //Der er 3 ting i gryden, og den skal nu kunne samles op.
                    pot.SetActive(false);
                    itemPrefab = Resources.Load<GameObject>("Prefabs/Pot");
                        CurrentItem = new GameHandler.Item(GameHandler.PossibleItems.finished, GameHandler.ItemState.none);
                        GrabItem();
                    //}
                }
            }
            else if (CurrentStand.tag == "CookFinish")
            {
                if (CurrentItem.possibleItems == GameHandler.PossibleItems.finished && playerRole == Role.cook)
                {
                    GameObject.Destroy(holdingItem);
                    pot.SetActive(true);
                    CurrentItem = emptyItem;
                    GameObject.Destroy(currentOrder);
                    gameHandlerObject.GetComponent<GameHandler>().dirtyPlateAmount += 1;

                }
            }
            else if (CurrentStand.tag == "WasherFinish")
            {
                if (CurrentItem.possibleItems == GameHandler.PossibleItems.dirtyPlate && CurrentItem.itemState == GameHandler.ItemState.clean && playerRole == Role.cleaner)
                {
                    GameObject.Destroy(holdingItem);
                    CurrentItem = emptyItem;
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
}
