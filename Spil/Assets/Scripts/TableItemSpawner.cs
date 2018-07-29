using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableItemSpawner : MonoBehaviour {

    public GameObject[] tables;
    public GameObject GameManager;
    public int spawnedDirtyPlates = 0;
    public int spawnedFoodPlates = 0;

    // Use this for initialization
    void Start () {

        tables = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            tables[i] = transform.GetChild(i).gameObject;
        }

    }
	
	// Update is called once per frame
	void Update () {
        SpawnDirtyPlates();
        SpawnFoodPlate();
    }

    public void SpawnDirtyPlates()
    {
        int dirtyPlateCount = GameManager.GetComponent<GameHandler>().dirtyPlateAmount;
        if (spawnedDirtyPlates < dirtyPlateCount)
        {
            int randomTable = Random.Range(0, tables.Length);
            if (tables[randomTable].GetComponent<SingleTableManager>().objectOnTable.possibleItems == GameHandler.PossibleItems.empty)
            {
                tables[randomTable].GetComponent<SingleTableManager>().objectOnTable = new GameHandler.Item(GameHandler.PossibleItems.dirtyPlate, GameHandler.ItemState.dirty);
                spawnedDirtyPlates++;
            }
        }
    }

    void SpawnFoodPlate()
    {
        int foodPlateCount = GameManager.GetComponent<GameHandler>().foodPlateAmount;
        if (spawnedFoodPlates < foodPlateCount)
        {
            int randomTable = Random.Range(0, tables.Length);
            if (tables[randomTable].GetComponent<SingleTableManager>().objectOnTable.possibleItems == GameHandler.PossibleItems.empty)
            {
                tables[randomTable].GetComponent<SingleTableManager>().objectOnTable = new GameHandler.Item(GameHandler.PossibleItems.foodPlate, GameHandler.ItemState.raw);
                spawnedFoodPlates++;
            }
        }
    }
}
