﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableTileCollision : MonoBehaviour {

    public GameObject key;
    public Transform tableParent;

    void Start()
    {
        tableParent = transform.parent;
    }
}
