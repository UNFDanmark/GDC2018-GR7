using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableItemSpawner : MonoBehaviour {

    public GameObject[] tables;
    public GameObject GameManager;
    public static int spawnedDirtyPlates = 0;
    public static int spawnedFoodPlates = 0;

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
        int dirtyPlateCount = GameManager.GetComponent<GameHandler>().dirtyPlateSpawnAmount;
        if (spawnedDirtyPlates < dirtyPlateCount)
        {
            int randomTable = Random.Range(0, tables.Length);
            if (tables[randomTable].GetComponent<SingleTableManager>().objectOnTable.possibleItems == GameHandler.PossibleItems.empty)
            {
                tables[randomTable].GetComponent<SingleTableManager>().objectOnTable = new GameHandler.Item(GameHandler.PossibleItems.dirtyPlate, GameHandler.ItemState.dirty, GameHandler.ItemPrefabDir.dirtyPlate);
                spawnedDirtyPlates++;
                GameManager.GetComponent<GameHandler>().dirtyPlateCounter++;
            }
        }
    }

    public void SpawnFoodPlate()
    {
        int foodPlateCount = GameManager.GetComponent<GameHandler>().foodPlateSpawnAmount;
        if (spawnedFoodPlates < foodPlateCount)
        {
            int randomTable = Random.Range(0, tables.Length);
            if (tables[randomTable].GetComponent<SingleTableManager>().objectOnTable.possibleItems == GameHandler.PossibleItems.empty)
            {
                tables[randomTable].GetComponent<SingleTableManager>().objectOnTable = new GameHandler.Item(GameHandler.PossibleItems.foodPlate, GameHandler.ItemState.raw, GameHandler.ItemPrefabDir.foodPlate);
                spawnedFoodPlates++;
            }
        }
    }
}
