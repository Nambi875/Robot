using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepController : MonoBehaviour
{
    public AudioSource footstepSource; // 발소리 오디오 소스
    public float footstepDelay = 0.5f; // 발소리 간격 조절
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

        // 밟고 있는 오브젝트의 태그에 따라 다른 소리를 재생
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
                // Grass 발소리 클립 반환
                return grassFootstepClip;
            default:
                return null;
        }
    }
}
