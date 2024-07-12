using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandFollowMouse : MonoBehaviour
{
    public Transform characterTransform;  // 캐릭터의 Transform 컴포넌트를 참조합니다.
    public Transform handTransform;       // 손의 Transform 컴포넌트를 참조합니다.
    public Camera mainCamera;             // 메인 카메라를 참조합니다.
    public float radius = 1.0f;           // 손이 이동할 반지름을 설정합니다.
    public Vector3 centerOffset;          // 손의 중심 위치를 설정합니다.
    public SpriteRenderer handSprite;     // 손의 SpriteRenderer를 참조합니다.
    public SpriteRenderer playerSprite;   // 플레이어의 SpriteRenderer를 참조합니다.

    void Update()
    {
        // 마우스 위치를 가져옵니다.
        Vector3 mousePosition = Input.mousePosition;

        // 마우스 위치를 월드 좌표로 변환합니다.
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, mainCamera.nearClipPlane));

        // 캐릭터의 중심 위치를 설정합니다.
        Vector3 centerPosition = characterTransform.position + centerOffset;

        // 마우스와 캐릭터 중심 위치 간의 방향을 계산합니다.
        Vector3 direction = worldPosition - centerPosition;
        direction.z = 0;  // 2D 게임이므로 z 값을 0으로 설정합니다.
        direction.Normalize();  // 방향 벡터를 정규화합니다.

        // 손의 위치를 계산합니다.
        Vector3 handPosition = centerPosition + direction * radius;
        handTransform.position = handPosition;

        // 손의 회전 각도를 계산하여 설정합니다.
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        handTransform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // 손과 캐릭터의 스프라이트를 반전하여 올바른 방향을 보도록 설정합니다.
        if (handPosition.x < characterTransform.position.x)
        {
            handSprite.flipY = true;
            playerSprite.flipX = true;
        }
        else
        {
            handSprite.flipY = false;
            playerSprite.flipX = false;
        }
    }
}