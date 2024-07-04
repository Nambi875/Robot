using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public float moveSpeed;
    public DialogueManager manager; 
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

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Start"))
        {
            moveSpeed = 0.0f;
        }
        else
        {
            moveSpeed = 3.0f;
        }
    }
    private void FixedUpdate()
    {
        //Ray

    }
}
