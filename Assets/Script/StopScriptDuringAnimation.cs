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
        // "Player" ｿﾀｺ・ｧﾆ?｡ｼ?Animator ﾄﾄﾆﾍﾆｮｸｦ ｰ｡ﾁｮｿﾉｴﾏｴﾙ.
        GameObject playerObject = GameObject.Find("Player");
        if (playerObject != null)
        {
            externalAnimator = playerObject.GetComponent<Animator>();

            // externalAnimatorｰ｡ ﾁｸﾀ酩ﾏｴﾂﾁ・ｴﾙｽﾃ ﾈｮﾀﾎﾇﾕｴﾏｴﾙ.
            if (externalAnimator == null)
            {
                Debug.LogError("Player ｿﾀｺ・ｧﾆ??Animator ﾄﾄﾆﾍﾆｮｰ｡ ｾﾀｴﾏｴﾙ!");
            }
        }
        else
        {
            Debug.LogError("\"Player\" ｿﾀｺ・ｧﾆ??ﾃ｣ﾀｻ ｼ・ｾﾀｴﾏｴﾙ!");
        }

        // ﾇ・ｿﾀｺ・ｧﾆ?｡ｼ?SpriteRendererｿﾍ HandFollowMouse ﾄﾄﾆﾍﾆｮｸｦ ｰ｡ﾁｮｿﾉｴﾏｴﾙ.
        spriteRenderer = GetComponent<SpriteRenderer>();
        handFollowMouse = GetComponent<HandFollowMouse>();

        // ﾄﾄﾆﾍﾆｮｰ｡ ﾁｸﾀ酩ﾏｴﾂﾁ・ﾈｮﾀﾎﾇﾕｴﾏｴﾙ.
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer ﾄﾄﾆﾍﾆｮｰ｡ ｾﾀｴﾏｴﾙ!");
        }
        if (handFollowMouse == null)
        {
            Debug.LogError("HandFollowMouse ﾄﾄﾆﾍﾆｮｰ｡ ｾﾀｴﾏｴﾙ!");
        }
    }

    // Updateｴﾂ ｸﾅ ﾇﾁｷｹﾀﾓｸｶｴﾙ ﾈ｣ﾃ箏ﾋｴﾏｴﾙ.
    void Update()
    {
        // Animatorﾀﾇ ﾇ・ｻﾂｸｦ ﾈｮﾀﾎﾇﾕｴﾏｴﾙ.
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
