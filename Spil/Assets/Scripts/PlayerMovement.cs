using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody rb;
    public float speed = 10f;

    Vector2 dir;

    public string horizontal = "p1H", vertical = "p1V";

    private void Start()
    {

        rb = GetComponent<Rigidbody>();

    }

    private void Update()
    {

        dir = new Vector2(Input.GetAxisRaw(horizontal), Input.GetAxisRaw(vertical));

    }

    private void FixedUpdate()
    {

        rb.velocity = (Vector3.forward * dir.x + Vector3.right * dir.y) * speed;

    }

}
