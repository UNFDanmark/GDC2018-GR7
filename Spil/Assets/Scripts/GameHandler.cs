using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour {
    
    public int foodPlateAmount;
    public int dirtyPlateAmount;
    public GameObject foodPlate;
    public GameObject dirtyPlate;
    public Item emptyItem = new Item(PossibleItems.empty, ItemState.none);

    public enum ItemState
    {

        raw,
        prept,
        done,
        burned,
        dirty,
        clean,
        none

    };

    public enum PossibleItems
    {
        empty,
        dirtyPlate,
        foodPlate
    };

    public class Item
    {
        public PossibleItems possibleItems;
        public ItemState itemState;

        public Item(PossibleItems itemType, ItemState stateOfItem)
        {
            possibleItems = itemType;
            itemState = stateOfItem;
        }
    }

}
