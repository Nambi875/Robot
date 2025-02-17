using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HandGun : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        HandleShooting();
    }
    private void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Shoot");
        }
    }
}
