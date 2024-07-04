using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    float speedX, speedY;
    bool isstart;
    Rigidbody2D rb;
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        anim.SetFloat("Speed", rb.velocity.magnitude);
        isstart = anim.GetBool("IsStart?");
        speedX = Input.GetAxisRaw("Horizontal") * moveSpeed;
        speedY = Input.GetAxisRaw("Vertical") * moveSpeed;
        rb.velocity = new Vector2(speedX, speedY);

        if (!isstart)
        {
            anim.SetBool("IsStart?", true);


        }
    }
}
