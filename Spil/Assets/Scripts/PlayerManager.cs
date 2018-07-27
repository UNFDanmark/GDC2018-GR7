using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public Role playerRole = Role.cook;
    public GameObject ActiveTile;
    public GameObject key;
    public bool onTile;

    public string horizontal = "p1H", vertical = "p1V";

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

        key.SetActive(onTile);

	}
}
