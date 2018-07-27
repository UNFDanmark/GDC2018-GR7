using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody rb;
    public float speed = 10f;

    Vector2 dir;

    private PlayerManager player;

    private void Start()
    {
        player = GetComponent<PlayerManager>();
        rb = GetComponent<Rigidbody>();

    }

    private void Update()
    {

        dir = new Vector2(Input.GetAxisRaw(player.horizontal), Input.GetAxisRaw(player.vertical));

    }

    private void FixedUpdate()
    {

        rb.velocity = (Vector3.forward * dir.x + Vector3.right * dir.y) * speed;

    }

}
