using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public float playerSpeed;
public float jumpSpeed;
public Animator animator;

private bool isJumping;
private float move;
private Rigidbody2D rb;
bool Jump = false;




// Start is called before the first frame update
void Start()
{
    rb = GetComponent<Rigidbody2D>();

}

// Update is called once per frame
void Update()
{
    move = Input.GetAxis("Horizontal");

    animator.SetFloat("Speed", Mathf.Abs(move));

    rb.velocity = new Vector2(move * playerSpeed, rb.velocity.y);

    if (move < 0)
    {
        transform.eulerAngles = new Vector3(0, 180, 0);
    }
    else if (move > 0)
    {
        transform.eulerAngles = new Vector3(0, 0, 0);
    }

    if (Input.GetButtonDown("Jump"))
    {
        ;
    }

    {
        if(Input.GetButtonDown("Jump")&& !isJumping)
    {
            rb.AddForce(new Vector2(rb.velocity.x, jumpSpeed));
            IsJumping = true;
            animator.SetBool("isJumping", true);
    }

}

    private void OnCollisionEnter2D(Collision2D other)
{
        if (other.gameObject.CompareTag("terrain_example", "terrain_example(1)", "terrain_example(2)", "terrain_example(3)", "terrain_example(4)"));
    {
        isJumping = false;
    }
    if (other.gameObject.CompareTag("Obstacles"))
    {
        isJumping = false;
    }
}  
    
}
