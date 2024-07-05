using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon1 : MonoBehaviour
{

    public GameObject weapon1;
    private bool isPlayerInZone = false;

    private void Update()
    {
        if (isPlayerInZone && Input.GetKeyDown(KeyCode.E))
        {
            GameObject.Find("Getweapons").GetComponent<PlayerWeaponSystem>().Getweapons = true;
            weapon1.SetActive(false);
            Debug.Log("Get Weapon Toggled: ");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInZone = true;
            Debug.Log("Player Entered Trigger Zone");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInZone = false;
            StopAllCoroutines(); 
            Debug.Log("Player Exited Trigger Zone");
        }
    }
}

