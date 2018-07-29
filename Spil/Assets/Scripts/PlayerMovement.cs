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
        if (player.canMove)
            dir = new Vector2(Input.GetAxisRaw(player.horizontal), Input.GetAxisRaw(player.vertical));
        else
            dir = Vector2.zero;

        RotatePlayer();    
    }

    private void FixedUpdate()
    {

        rb.velocity = (Vector3.forward * dir.x + Vector3.right * dir.y) * speed;

    }

    private void RotatePlayer()
    {
        Debug.Log("Horizontal axis: " + Input.GetAxisRaw(player.horizontal) + " Vertical axis: " + Input.GetAxisRaw(player.vertical));

        float HorizontalAxis = Input.GetAxisRaw(player.horizontal);
        float VerticalAxis = Input.GetAxisRaw(player.vertical);
        
        //if statements spam

        if (HorizontalAxis == 0 && VerticalAxis == -1)
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        else if (HorizontalAxis == 1 && VerticalAxis == -1)
        {
            transform.rotation = Quaternion.Euler(0, -45, 0);
        }
        else if (HorizontalAxis == 1 && VerticalAxis == 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (HorizontalAxis == 1 && VerticalAxis == 1)
        {
            transform.rotation = Quaternion.Euler(0, 45, 0);
        }
        else if (HorizontalAxis == 0 && VerticalAxis == 1)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else if (HorizontalAxis == -1 && VerticalAxis == 1)
        {
            transform.rotation = Quaternion.Euler(0, -225, 0);
        }
        else if (HorizontalAxis == -1 && VerticalAxis == 0)
        {
            transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else if (HorizontalAxis == -1 && VerticalAxis == -1)
        {
            transform.rotation = Quaternion.Euler(0, -135, 0);
        }


    }

}
