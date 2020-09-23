using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCtrl2 : MonoBehaviour
{
 //creates a float variable that can be accessed everywhere.  Allows the adjustment of speed of movement
 public float playerSpeed;
 //creates a float variable that can be accessed everywhere.  Allows the adjustment of jump speed of the player
 private Rigidbody2D rb;
    //Allows me to decide which key is left and which key is right in unity
    public KeyCode left;
    public KeyCode right;

    void Start()
    {
        //Brings rigidbody2D in
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {

        //States what happens when the left and right key that will be assigned is pressed.
        if (Input.GetKey(left))
        {
            rb.velocity = new Vector2(-playerSpeed, rb.velocity.y);
        }
        else if (Input.GetKey(right))
        {
            rb.velocity = new Vector2(playerSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
      
    }
}
