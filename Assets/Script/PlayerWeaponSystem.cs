using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponSystem : MonoBehaviour
{
    public GameObject hand;
    public GameObject HandGun;
    public bool Getweapons;

    private void Start()
    {
        Getweapons = false;
        hand.SetActive(true);
        HandGun.SetActive(false);
    }

    private void Update()
    {
        if (Getweapons == true)
        {
            hand.SetActive(false);
            HandGun.SetActive(true);
        }
        else
        {
            hand.SetActive(true);
            HandGun.SetActive(false);
        }
    }
}
