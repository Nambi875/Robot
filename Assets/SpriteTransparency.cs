using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteTransparency : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // 스프라이트 렌더러 컴포넌트를 가져옵니다.
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // 현재 색상을 가져옵니다.
        Color color = spriteRenderer.color;

        // Alpha 값을 변경하여 투명도를 조정합니다.
        // Alpha 값은 0(완전히 투명)에서 1(완전히 불투명) 사이입니다.
        color.a = 0.5f; // 50% 투명하게 설정

        // 변경된 색상을 다시 설정합니다.
        spriteRenderer.color = color;
    }
}