using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControler : MonoBehaviour
{
    public GameObject bulletPrefab;  // �Ѿ� ������
    public Transform firePoint;      // �߻�E����
    public float bulletSpeed = 20f;  // �Ѿ� �ӵ�
    public float fireRate = 0.5f;    // �߻�E����
    public float reloadTime = 2.0f;  // ������E�ð�
    public int maxAmmo = 10;         // �ִ�Eź��E��E
    private int currentAmmo;         // ����Eź��E��E
    private float nextFireTime = 0f; // ���� �߻�E���� �ð�
    private bool canFire = true;     // �߻�E���� ����
    private bool isShooting = false; // �߻�E�� ����
    private Animator anim;

    private Queue<float> fireQueue = new Queue<float>(); // �߻�Eť

    void Start()
    {
        // ź��E�ʱ�ȭ
        currentAmmo = maxAmmo;
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // ����E� ���� ��ư�� ������ �� �߻�E
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

        // R Ű�� ������ �� ������E
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!isShooting)  // �߻�E���� �ƴ� ���� ������E����
            {
                StartCoroutine(Reload());
                GetComponent<Animator>().SetTrigger("Reload");
                GetComponent<Animator>().SetBool("NeedReload", false);
            }
        }
    }

    // �߻�E
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

    // ������E
    IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        canFire = true;
        GetComponent<Animator>().SetBool("NeedReload", false);
    }
}