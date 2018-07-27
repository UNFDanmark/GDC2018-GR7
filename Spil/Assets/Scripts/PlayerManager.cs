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

    public enum Role
    {
       cook,
       cleaner
    }


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        Debug.Log(CurrentItem);

        key.SetActive(onTile);
        if (onTile)
        {

            if (Input.GetButtonDown(action))
            {

                if (CurrentStand.GetComponent<SingleTableManager>().objectOnTable != PossibleItems.empty)
                {

                    if (CurrentStand.GetComponent<SingleTableManager>().objectOnTable == PossibleItems.foodPlate)
                    {

                        itemPrefab = Resources.Load<GameObject>("Prefabs/foodPlate");

                    }

                    CurrentItem = CurrentStand.GetComponent<SingleTableManager>().objectOnTable;
                    CurrentStand.GetComponent<SingleTableManager>().objectOnTable = PossibleItems.empty;
                    GrabItem();

                } else if (CurrentStand.GetComponent<SingleTableManager>().objectOnTable == PossibleItems.empty && CurrentItem != PossibleItems.empty)
                {

                    //Place holded item on table
                    GameObject.Destroy(holdingItem);
                    CurrentStand.GetComponent<SingleTableManager>().objectOnTable = PossibleItems.foodPlate;
                    CurrentStand.GetComponent<SingleTableManager>().emptyTable = true;

                }

            }

        }

	}

    void GrabItem()
    {

        Vector3 offset = new Vector3(0, 0, 0);
        offset.x = 1;
        holdingItem = Instantiate(itemPrefab, transform.position + offset, Quaternion.Euler(0, 0, 0), transform);

    }
}
