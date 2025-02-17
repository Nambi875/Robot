using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletControler : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 20f;
    public float fireRate = 0.5f;
    public float reloadTime = 2.0f;
    public int maxAmmo = 10;
    public Slider reloadSlider;

    private int currentAmmo;
    private float nextFireTime = 0f;
    private bool canFire = true;
    private bool isShooting = false;
    private bool isReloading = false;
    private Animator anim;
    AllAudio allAudio;
    private Queue<float> fireQueue = new Queue<float>();

    void Start()
    {
        currentAmmo = maxAmmo;
        if (reloadSlider != null)
        {
            reloadSlider.gameObject.SetActive(false);
        }
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        allAudio = GameObject.FindGameObjectWithTag("Audio").GetComponent<AllAudio>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (Time.time >= nextFireTime)
            {
                if (canFire && !isShooting && !isReloading)
                {
                    GetComponent<Animator>().SetBool("NeedReload", false);
                    nextFireTime = Time.time + fireRate;
                    GetComponent<Animator>().SetTrigger("Shoot");
                    StartCoroutine(Shoot());
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!isShooting && !isReloading)
            {
                StartCoroutine(Reload());
                GetComponent<Animator>().SetTrigger("Reload");
                GetComponent<Animator>().SetBool("NeedReload", false);
            }
        }
    }

    IEnumerator Shoot()
    {
        isShooting = true;
        allAudio.PlaySFX(allAudio.firesound);
        if (canFire)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = firePoint.right * bulletSpeed;
            currentAmmo--;
            if (currentAmmo <= 0)
            {
                canFire = false;
                GetComponent<Animator>().SetBool("NeedReload", true);
            }
        }
        yield return new WaitForSeconds(fireRate);
        isShooting = false;
    }

    IEnumerator Reload()
    {
        isReloading = true;
        allAudio.PlaySFX(allAudio.reloading);
        if (reloadSlider != null)
        {
            reloadSlider.gameObject.SetActive(true);
            reloadSlider.value = 0f;
        }

        float elapsedTime = 0f;
        while (elapsedTime < reloadTime)
        {
            elapsedTime += Time.deltaTime;
            if (reloadSlider != null)
            {
                reloadSlider.value = elapsedTime / reloadTime;
            }
            yield return null;
        }

        currentAmmo = maxAmmo;
        canFire = true;
        isReloading = false;
        GetComponent<Animator>().SetBool("NeedReload", false);

        if (reloadSlider != null)
        {
            reloadSlider.gameObject.SetActive(false);
        }
    }
}