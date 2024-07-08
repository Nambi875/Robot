using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControler : MonoBehaviour
{
    public GameObject bulletPrefab;  // ?? ???
    public Transform firePoint;      // ?? ?? ??
    public float bulletSpeed = 20f;  // ?? ??
    public float fireRate = 0.5f;    // ?? ?? (? ??)
    public float reloadTime = 2.0f; // ???? ??? ?? (? ??)
    public int maxAmmo = 10;         // ?? ?? ?
    private float nextFireTime = 0f; // ?? ?? ??
    private bool canFire = true;     // ?? ?? ??
    private int currentAmmo;         // ?? ?? ?
    private Animator anim;

    private Queue<float> fireQueue = new Queue<float>(); // ?? ?

    void Start()
    {
        // ?? ?? ? ??
        currentAmmo = maxAmmo;
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // ??? ?? ? ?? ?? ?? ??
        if (Input.GetMouseButton(0))
        {
            if (Time.time >= nextFireTime)
            {
                if (canFire)
                {
                    GetComponent<Animator>().SetBool("NeedReload", false);
                    nextFireTime = Time.time + fireRate;
                    GetComponent<Animator>().SetTrigger("Shoot");
                    Shoot();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
            GetComponent<Animator>().SetTrigger("Reload");
            GetComponent<Animator>().SetBool("NeedReload", false);
        }
    }
    // ????? ???? ?? ???? ??
    void Shoot()
    {
        if (canFire)
        {
            // ?? ??
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            // ??? Rigidbody2D? ???
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            // ??? ?? ?? ??
            rb.velocity = firePoint.right * bulletSpeed;

            currentAmmo--;

            if (currentAmmo <= 0)
            {
                canFire = false;
                GetComponent<Animator>().SetBool("NeedReload", true);
            }
        }
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        canFire = true;
        GetComponent<Animator>().SetBool("NeedReload", false);
    }
}
