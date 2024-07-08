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
        // "Player" 오브젝트에서 Animator 컴포넌트를 가져옵니다.
        GameObject playerObject = GameObject.Find("Player");
        if (playerObject != null)
        {
            externalAnimator = playerObject.GetComponent<Animator>();

            // externalAnimator가 존재하는지 다시 확인합니다.
            if (externalAnimator == null)
            {
                Debug.LogError("Player 오브젝트에 Animator 컴포넌트가 없습니다!");
            }
        }
        else
        {
            Debug.LogError("\"Player\" 오브젝트를 찾을 수 없습니다!");
        }

        // 현재 오브젝트에서 SpriteRenderer와 HandFollowMouse 컴포넌트를 가져옵니다.
        spriteRenderer = GetComponent<SpriteRenderer>();
        handFollowMouse = GetComponent<HandFollowMouse>();

        // 컴포넌트가 존재하는지 확인합니다.
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer 컴포넌트가 없습니다!");
        }
        if (handFollowMouse == null)
        {
            Debug.LogError("HandFollowMouse 컴포넌트가 없습니다!");
        }
    }

    // Update는 매 프레임마다 호출됩니다.
    void Update()
    {
        // Animator의 현재 상태를 확인합니다.
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
