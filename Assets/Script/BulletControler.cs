using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControler : MonoBehaviour
{
    public GameObject bulletPrefab;  // 총알 프리팹
    public Transform firePoint;      // 발사 지점
    public float bulletSpeed = 20f;  // 총알 속도
    public float fireRate = 0.5f;    // 발사 간격
    public float reloadTime = 2.0f;  // 재장전 시간
    public int maxAmmo = 10;         // 최대 탄약 수
    private int currentAmmo;         // 현재 탄약 수
    private float nextFireTime = 0f; // 다음 발사 가능 시간
    private bool canFire = true;     // 발사 가능 여부
    private bool isShooting = false; // 발사 중 여부
    private Animator anim;

    private Queue<float> fireQueue = new Queue<float>(); // 발사 큐

    void Start()
    {
        // 탄약 초기화
        currentAmmo = maxAmmo;
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // 마우스 왼쪽 버튼이 눌렸을 때 발사
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

        // R 키가 눌렸을 때 재장전
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!isShooting)  // 발사 중이 아닐 때만 재장전 시작
            {
                StartCoroutine(Reload());
                GetComponent<Animator>().SetTrigger("Reload");
                GetComponent<Animator>().SetBool("NeedReload", false);
            }
        }
    }

    // 발사
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

    // 재장전
    IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        canFire = true;
        GetComponent<Animator>().SetBool("NeedReload", false);
    }
}