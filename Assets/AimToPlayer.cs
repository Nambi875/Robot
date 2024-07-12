using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.XR;

public class AimToPlayer : MonoBehaviour
{
    public Transform player;
    public Vector3 centerOffset;          // 손의 중심 위치를 설정합니다.
    public float radius = 1.0f;           // 손이 이동할 반지름을 설정합니다.
    public float fireRate = 1f;
    public float shootRange = 10f;        // 사격 범위
    public Transform enemyBody;           // 적 본체를 참조하도록 추가
    public SpriteRenderer handSprite;     // 손의 SpriteRenderer를 참조합니다.

    void Update()
    {
        // 플레이어 위치를 월드 좌표로 변환
        Vector3 playerPosition = player.position;

        // 적 본체의 중심 위치 설정
        Vector3 centerPosition = enemyBody.position + centerOffset;

        // 플레이어와 적 본체 중심 위치 간의 방향 계산
        Vector3 direction = playerPosition - centerPosition;
        direction.z = 0;  // 2D 게임이므로 z 값을 0으로 설정
        direction.Normalize();  // 방향 벡터를 정규화

        // 총을 든 손의 위치 계산
        Vector3 handPosition = centerPosition + direction * radius;
        transform.position = handPosition;

        // 총을 든 손의 회전 각도 계산 및 설정
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // 손과 캐릭터의 스프라이트를 반전하여 올바른 방향을 보도록 설정합니다.
        if (handPosition.x < enemyBody.position.x)
        {
            handSprite.flipY = true;
        }
        else
        {
            handSprite.flipY = false;
        }

    }
}