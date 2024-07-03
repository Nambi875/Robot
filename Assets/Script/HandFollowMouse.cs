using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandFollowMouse : MonoBehaviour
{
    public Transform characterTransform;  // ??? ??? Transform? ?????.
    public Transform handTransform;       // ??? ?? Transform? ?????.
    public Camera mainCamera;             // ?? ???? ?????.
    public float radius = 1.0f;           // ?? ??? ???? ?????.
    public Vector3 centerOffset;          // ?? ?? ???? ?????.
    public SpriteRenderer handSprite;     // ?? ????? ???? ?????.

    void Update()
    {
        // ??? ??? ?????.
        Vector3 mousePosition = Input.mousePosition;

        // ??? ??? ?? ??? ?????.
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, mainCamera.nearClipPlane));

        // ??? ?? ??? ???????.
        Vector3 centerPosition = characterTransform.position + centerOffset;

        // ??? ??? ??? ?? ?? ??? ?????.
        Vector3 direction = worldPosition - centerPosition;
        direction.z = 0;  // 2D ??? ?? z ?? 0?? ?????.
        direction.Normalize();  // ?? ??? ??????.

        // ?? ??? ?? ??? ?? ???????.
        Vector3 handPosition = centerPosition + direction * radius;
        handTransform.position = handPosition;

        // ?? ??? ??? ????? ??????.
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        handTransform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // ?? ?? ?? ???? ?????? X?? ?????.
        if (handPosition.x < characterTransform.position.x)
        {
            handSprite.flipX = true;
        }
        else
        {
            handSprite.flipX = false;
        }
    }
}