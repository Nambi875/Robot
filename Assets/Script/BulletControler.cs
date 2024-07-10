using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControler : MonoBehaviour
{
    public GameObject bulletPrefab;  // ﾃﾑｾﾋ ﾇﾁｸｮﾆﾕ
    public Transform firePoint;      // ｹﾟｻ・ﾁ｡
    public float bulletSpeed = 20f;  // ﾃﾑｾﾋ ｼﾓｵｵ
    public float fireRate = 0.5f;    // ｹﾟｻ・ｰ｣ｰﾝ
    public float reloadTime = 2.0f;  // ﾀ鄲蠡・ｽﾃｰ｣
    public int maxAmmo = 10;         // ﾃﾖｴ・ﾅｺｾ・ｼ・
    private int currentAmmo;         // ﾇ・ﾅｺｾ・ｼ・
    private float nextFireTime = 0f; // ｴﾙﾀｽ ｹﾟｻ・ｰ｡ｴﾉ ｽﾃｰ｣
    private bool canFire = true;     // ｹﾟｻ・ｰ｡ｴﾉ ｿｩｺﾎ
    private bool isShooting = false; // ｹﾟｻ・ﾁﾟ ｿｩｺﾎ
    private Animator anim;

    private Queue<float> fireQueue = new Queue<float>(); // ｹﾟｻ・ﾅ･

    void Start()
    {
        // ﾅｺｾ・ﾃﾊｱ篳ｭ
        currentAmmo = maxAmmo;
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // ｸｶｿ・ｺ ｿﾞﾂﾊ ｹｰﾀﾌ ｴｭｷﾈﾀｻ ｶｧ ｹﾟｻ・
        if (Input.GetMouseButton(0))
        {
            if (Time.time >= nextFireTime)
            {
                if (canFire && !isShooting)
                {
                    GetComponent<Animator>().SetBool("NeedReload", false);
                    nextFireTime = Time.time + fireRate;
                    GetComponent<Animator>().SetTrigger("Shoot");
                    StartCoroutine(Shoot());
                }
            }
        }

        // R ﾅｰｰ｡ ｴｭｷﾈﾀｻ ｶｧ ﾀ鄲蠡・
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!isShooting)  // ｹﾟｻ・ﾁﾟﾀﾌ ｾﾆｴﾒ ｶｧｸｸ ﾀ鄲蠡・ｽﾃﾀﾛ
            {
                StartCoroutine(Reload());
                GetComponent<Animator>().SetTrigger("Reload");
                GetComponent<Animator>().SetBool("NeedReload", false);
            }
        }
    }

    // ｹﾟｻ・
    IEnumerator Shoot()
    {
        isShooting = true;

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

    // ﾀ鄲蠡・
    IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        canFire = true;
        GetComponent<Animator>().SetBool("NeedReload", false);
    }
}