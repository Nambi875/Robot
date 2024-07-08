using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopScriptDuringAnimation : MonoBehaviour
{
    public Animator externalAnimator;
    private SpriteRenderer spriteRenderer;
    private HandFollowMouse handFollowMouse;

    private void Awake()
    {
        // "Player" ������Ʈ���� Animator ������Ʈ�� �����ɴϴ�.
        GameObject playerObject = GameObject.Find("Player");
        if (playerObject != null)
        {
            externalAnimator = playerObject.GetComponent<Animator>();

            // externalAnimator�� �����ϴ��� �ٽ� Ȯ���մϴ�.
            if (externalAnimator == null)
            {
                Debug.LogError("Player ������Ʈ�� Animator ������Ʈ�� �����ϴ�!");
            }
        }
        else
        {
            Debug.LogError("\"Player\" ������Ʈ�� ã�� �� �����ϴ�!");
        }

        // ���� ������Ʈ���� SpriteRenderer�� HandFollowMouse ������Ʈ�� �����ɴϴ�.
        spriteRenderer = GetComponent<SpriteRenderer>();
        handFollowMouse = GetComponent<HandFollowMouse>();

        // ������Ʈ�� �����ϴ��� Ȯ���մϴ�.
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer ������Ʈ�� �����ϴ�!");
        }
        if (handFollowMouse == null)
        {
            Debug.LogError("HandFollowMouse ������Ʈ�� �����ϴ�!");
        }
    }

    // Update�� �� �����Ӹ��� ȣ��˴ϴ�.
    void Update()
    {
        // Animator�� ���� ���¸� Ȯ���մϴ�.
        if (externalAnimator != null && spriteRenderer != null && handFollowMouse != null)
        {
            if (externalAnimator.GetCurrentAnimatorStateInfo(0).IsName("Start"))
            {
                handFollowMouse.enabled = false;
                spriteRenderer.enabled = false;
            }
            else
            {
                handFollowMouse.enabled = true;
                spriteRenderer.enabled = true;
            }
        }
    }
}
