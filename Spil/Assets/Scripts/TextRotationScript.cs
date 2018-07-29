using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextRotationScript : MonoBehaviour {

    void Awake()
    {
        transform.LookAt(Camera.main.transform);
    }

}
