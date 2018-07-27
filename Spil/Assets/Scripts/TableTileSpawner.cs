using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableTileSpawner : MonoBehaviour {
    public GameObject TableTile;
    public Vector3 offset;
    public TableThing objectOnTable = TableThing.empty;
    

    public enum TableThing
    {
        empty,
        dirtyPlate,
        foodPlate
    };

	// Use this for initialization
	void Start ()
    {
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
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    
}
