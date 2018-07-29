using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour {
    
    public int foodPlateAmount;
    public int dirtyPlateAmount;
    public GameObject foodPlate;
    public GameObject dirtyPlate;
    public enum itemState
    {

        raw,
        prept,
        done,
        burned,
        dirty,
        clean

    };

}
