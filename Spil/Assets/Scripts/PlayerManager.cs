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
            Debug.Log(hit.collider.gameObject.tag);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);

            if(hit.collider.gameObject.tag == "Tile")
            {
                Debug.Log("på tile");
                //Player er på en tile
                onTile = true;
                CurrentStand = hit.collider.transform.parent.gameObject;

            }
            else
            {
                Debug.Log("Ikke på tile");
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
            if (CurrentStand.GetComponent<SingleTableManager>().objectOnTable != PossibleItems.empty)
            {

                if (CurrentStand.GetComponent<SingleTableManager>().objectOnTable == PossibleItems.foodPlate)
                {

                    itemPrefab = Resources.Load<GameObject>("Prefabs/foodPlate");

                }

                CurrentItem = CurrentStand.GetComponent<SingleTableManager>().objectOnTable;
                CurrentStand.GetComponent<SingleTableManager>().objectOnTable = PossibleItems.empty;
                GrabItem();

            }
            else if (CurrentStand.GetComponent<SingleTableManager>().objectOnTable == PossibleItems.empty && CurrentItem != PossibleItems.empty)
            {

                //Place holded item on table
                GameObject.Destroy(holdingItem);
                CurrentStand.GetComponent<SingleTableManager>().objectOnTable = PossibleItems.foodPlate;
                CurrentStand.GetComponent<SingleTableManager>().emptyTable = true;
                CurrentItem = PossibleItems.empty;

            }

        }
    }
}
