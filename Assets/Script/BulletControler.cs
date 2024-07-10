using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControler : MonoBehaviour
{
    public GameObject bulletPrefab;  // �Ѿ� ������
    public Transform firePoint;      // �߻� ����
    public float bulletSpeed = 20f;  // �Ѿ� �ӵ�
    public float fireRate = 0.5f;    // �߻� ����
    public float reloadTime = 2.0f;  // ������ �ð�
    public int maxAmmo = 10;         // �ִ� ź�� ��
    private int currentAmmo;         // ���� ź�� ��
    private float nextFireTime = 0f; // ���� �߻� ���� �ð�
    private bool canFire = true;     // �߻� ���� ����
    private bool isShooting = false; // �߻� �� ����
    private Animator anim;

    private Queue<float> fireQueue = new Queue<float>(); // �߻� ť

    void Start()
    {
        // ź�� �ʱ�ȭ
        currentAmmo = maxAmmo;
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // ���콺 ���� ��ư�� ������ �� �߻�
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

        // R Ű�� ������ �� ������
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!isShooting)  // �߻� ���� �ƴ� ���� ������ ����
            {
                StartCoroutine(Reload());
                GetComponent<Animator>().SetTrigger("Reload");
                GetComponent<Animator>().SetBool("NeedReload", false);
            }
        }
    }

    // �߻�
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

    // ������
    IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        canFire = true;
        GetComponent<Animator>().SetBool("NeedReload", false);
    }
}