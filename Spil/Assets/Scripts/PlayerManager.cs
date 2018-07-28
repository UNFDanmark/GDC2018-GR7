using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public Role playerRole = Role.cook;
    public GameObject key;
    public GameObject CurrentStand;
    public GameObject itemPrefab;
    public GameObject holdingItem;
    public PossibleItems CurrentItem = PossibleItems.empty;
    public bool onTile;

    public enum PossibleItems
    {
        empty,
        dirtyPlate,
        foodPlate
    };

    public string horizontal = "p1H", vertical = "p1V", action = "p1A";

    public GameObject UIkey, UIkey2;

    public enum Role
    {
       cook,
       cleaner
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

	}

    void GrabItem()
    {

        Vector3 offset = new Vector3(0, 0, 0);
        offset.x = 1;
        holdingItem = Instantiate(itemPrefab, transform.position + offset, Quaternion.Euler(0, 0, 0), transform);

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
                if (CurrentStand.GetComponent<SingleTableManager>().objectOnTable != PossibleItems.empty && CurrentItem == PossibleItems.empty)
                {

                    switch (CurrentStand.GetComponent<SingleTableManager>().objectOnTable)
                    {

                        case PossibleItems.dirtyPlate:
                            itemPrefab = Resources.Load<GameObject>("Prefabs/dirtyPlate");
                            break;
                        case PossibleItems.foodPlate:
                            itemPrefab = Resources.Load<GameObject>("Prefabs/foodPlate");
                            break;

                    }

                    CurrentItem = CurrentStand.GetComponent<SingleTableManager>().objectOnTable;
                    CurrentStand.GetComponent<SingleTableManager>().objectOnTable = PossibleItems.empty;
                    GrabItem();

                }
                else if (CurrentStand.GetComponent<SingleTableManager>().objectOnTable == PossibleItems.empty && CurrentItem != PossibleItems.empty)
                {

                    //Place holded item on table
                    GameObject.Destroy(holdingItem);
                    CurrentStand.GetComponent<SingleTableManager>().objectOnTable = CurrentItem;
                    CurrentStand.GetComponent<SingleTableManager>().emptyTable = true;
                    CurrentItem = PossibleItems.empty;

                }
            }
            else if (CurrentStand.tag == "Stove")
            {
                //Gør noget med stove
                if (CurrentItem == PossibleItems.foodPlate)
                {
                    if (CurrentStand.GetComponent<SingleStoveManager>().itemInStove == PossibleItems.empty)
                    {
                        
                        CurrentStand.GetComponent<SingleStoveManager>().itemInStove = PossibleItems.foodPlate;
                        CurrentItem = PossibleItems.empty;
                        GameObject.Destroy(holdingItem);

                    }
                }
                else if (CurrentItem == PossibleItems.empty && CurrentStand.GetComponent<SingleStoveManager>().itemInStove == PossibleItems.foodPlate && CurrentStand.GetComponent<SingleStoveManager>().stoveDone)
                {
                    CurrentItem = PossibleItems.foodPlate;
                    CurrentStand.GetComponent<SingleStoveManager>().itemInStove = PossibleItems.empty;
                    itemPrefab = Resources.Load<GameObject>("Prefabs/foodPlate");
                    GrabItem();
                }

            }
            else if (CurrentStand.tag == "Washer")
            {
                //Gør noget med stove
                if (CurrentItem == PossibleItems.dirtyPlate)
                {
                    if (CurrentStand.GetComponent<SingleWasherManager>().itemInWasher == PossibleItems.empty)
                    {

                        CurrentStand.GetComponent<SingleWasherManager>().itemInWasher = PossibleItems.dirtyPlate;
                        CurrentItem = PossibleItems.empty;
                        GameObject.Destroy(holdingItem);

                    }
                }
                else if (CurrentItem == PossibleItems.empty && CurrentStand.GetComponent<SingleWasherManager>().itemInWasher == PossibleItems.dirtyPlate && CurrentStand.GetComponent<SingleWasherManager>().washerDone)
                {
                    CurrentItem = PossibleItems.dirtyPlate;
                    CurrentStand.GetComponent<SingleWasherManager>().itemInWasher = PossibleItems.empty;
                    itemPrefab = Resources.Load<GameObject>("Prefabs/dirtyPlate");
                    GrabItem();
                }

            }
        }
    }
}
