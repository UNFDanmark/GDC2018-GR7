using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public Role playerRole = Role.cook;
    public GameObject key;
    public GameObject CurrentStand;
    public GameObject itemPrefab;
    public GameObject holdingItem;
    public GameHandler.Item CurrentItem;
    public bool onTile;
    public bool canMove = true;
    public bool quickEventActive = false;
    public Transform itemPlacement;
    public GameHandler.Item emptyItem = new GameHandler.Item(GameHandler.PossibleItems.empty, GameHandler.ItemState.none);



    public string horizontal = "p1H", vertical = "p1V", action = "p1A";

    public GameObject UIkey, UIkey2;

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

        TileChecker();
        key.SetActive(onTile);
        if (onTile)
        {

            if (Input.GetButtonDown(action))
            {
                Actions();
            }

        }
        Debug.Log("Current item er " + CurrentItem.possibleItems);

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
                Debug.Log((CurrentStand.GetComponent<SingleTableManager>().objectOnTable.possibleItems == GameHandler.PossibleItems.empty) + " " + CurrentItem.possibleItems);
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
                    Debug.Log("Tog item fra bord: " + CurrentItem.possibleItems);

                }
                else if (CurrentStand.GetComponent<SingleTableManager>().objectOnTable.possibleItems == GameHandler.PossibleItems.empty && CurrentItem.possibleItems != GameHandler.PossibleItems.empty)
                {
                    Debug.Log("Her");
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
               if (CurrentItem.possibleItems == GameHandler.PossibleItems.foodPlate)
                {
                    if (!quickEventActive)
                    {
                        canMove = false;
                        quickEventActive = true;
                        KeyManager.TriggerQuickEvent(this, 30, QuickEventOver);
                    }
                }
            }
        }
    }

    public void QuickEventOver()
    {
        canMove = true;
        quickEventActive = false;

        // Rimeligt hardcoded. Ændrer dette hvis at vi bruger quicktime til andre ting end at washe og cutte
        //if (CurrentItem.possibleItems)

    }
}
