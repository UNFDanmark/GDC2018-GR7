using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTableManager : MonoBehaviour {
    public GameObject TableTile;
    public GameObject foodPlate;
    public GameObject dirtyPlate;
    public GameObject currentitem;
    public bool emptyTable = true;
    public GameHandler.Item objectOnTable = new GameHandler.Item(GameHandler.PossibleItems.empty, GameHandler.ItemState.none, GameHandler.ItemPrefabDir.none);
    



	// Use this for initialization
	void Start ()
    {

        Vector3 offset = new Vector3(0,0,0);

        offset.y = -0.49f;

        offset.x = 1;
        GameObject newTile;
        newTile =  Instantiate(TableTile, transform.position + offset,Quaternion.Euler(90,0,0),transform);
        offset.x = -1;
        newTile = Instantiate(TableTile, transform.position + offset, Quaternion.Euler(90, 0, 0), transform);

        offset.x = 0;
        offset.z = 1;
        newTile = Instantiate(TableTile, transform.position + offset, Quaternion.Euler(90, 0, 0), transform);
        offset.z = -1;
        newTile = Instantiate(TableTile, transform.position + offset, Quaternion.Euler(90, 0, 0), transform);

        SpawnItem();

    }
	
	// Update is called once per frame
	void Update () {

        SpawnItem();

    }

    void SpawnItem()
    {

        if (objectOnTable.possibleItems == GameHandler.PossibleItems.foodPlate && emptyTable)
        {

            Vector3 offset = new Vector3(0, 0, 0);
            offset.y = 1;
            GameObject itemPrefab = Resources.Load<GameObject>("Prefabs/" + objectOnTable.prefabDir.ToString());
            currentitem = Instantiate(itemPrefab, transform.position + offset, Quaternion.identity, transform);
            emptyTable = false;

        }
        else if (objectOnTable.possibleItems == GameHandler.PossibleItems.dirtyPlate && emptyTable)
        {

            Vector3 offset = new Vector3(0, 0, 0);
            offset.y = 1;
            GameObject itemPrefab = Resources.Load<GameObject>("Prefabs/" + objectOnTable.prefabDir.ToString());
            currentitem = Instantiate(itemPrefab, transform.position + offset, Quaternion.identity, transform);
            emptyTable = false;

        }

        else if (objectOnTable.possibleItems == GameHandler.PossibleItems.empty)
        {

            GameObject.Destroy(currentitem);

        }

    }
    
}
