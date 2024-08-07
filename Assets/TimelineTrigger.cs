using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineTrigger : MonoBehaviour
{
    public PlayableDirector timelineDirector; // 재생할 타임라인
    private bool hasPlayed = false; // 타임라인이 재생되었는지 여부

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasPlayed)
        {
            // 플레이어가 트리거에 진입하고 타임라인이 아직 재생되지 않은 경우
            if (timelineDirector != null)
            {
                timelineDirector.Play();
                hasPlayed = true; // 타임라인 재생 상태 업데이트
                Debug.Log("TimelineTrigger: Player entered, timeline playing");
            }
        }
    }
}