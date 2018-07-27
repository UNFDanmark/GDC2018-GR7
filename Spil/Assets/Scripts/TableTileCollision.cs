using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableTileCollision : MonoBehaviour {

    public GameObject key;
    public Transform tableParent;

    private void Start()
    {
        tableParent = transform.parent;
    }

    private void OnTriggerStay(Collider other)
    {
        
        if (other.gameObject.tag == "Player")
        {

            other.GetComponent<PlayerManager>().onTile = true;
            other.GetComponent<PlayerManager>().CurrentStand = transform.parent.gameObject;

        }

    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.gameObject.tag == "Player")
        {

            other.GetComponent<PlayerManager>().onTile = false;
            other.GetComponent<PlayerManager>().CurrentStand = null;

        }

    }

}
