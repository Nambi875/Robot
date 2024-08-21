using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepController : MonoBehaviour
{
    public AudioSource footstepSource; // �߼Ҹ� ����� �ҽ�
    public float footstepDelay = 0.5f; // �߼Ҹ� ���� ����
    public AudioClip grassFootstepClip;
    private CharacterController controller;
    private bool isMoving = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (controller.velocity.magnitude > 0.1f && !isMoving)
        {
            StartCoroutine(PlayFootstepSound());
        }
    }

    private IEnumerator PlayFootstepSound()
    {
        isMoving = true;

        // ��� �ִ� ������Ʈ�� �±׿� ���� �ٸ� �Ҹ��� ���
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f);
        if (hit.collider != null)
        {
            string tag = hit.collider.tag;
            AudioClip clip = GetFootstepClip(tag);
            if (clip != null)
            {
                footstepSource.clip = clip;
                footstepSource.Play();
            }
        }

        yield return new WaitForSeconds(footstepDelay);
        isMoving = false;
    }

    private AudioClip GetFootstepClip(string tag)
    {
        switch (tag)
        {
            case "Grass":
                // Grass �߼Ҹ� Ŭ�� ��ȯ
                return grassFootstepClip;
            default:
                return null;
        }
    }
}
