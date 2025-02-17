using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineTrigger : MonoBehaviour
{
    public string timelineID; // 타임라인 고유 ID
    public PlayableDirector timelineDirector; // 재생할 타임라인
    private TimelineManager timelineManager;
    private bool hasPlayed = false;

    private void Start()
    {
        timelineManager = FindObjectOfType<TimelineManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 트리거에 들어온 객체가 "Player" 태그를 가진 경우에만 실행
        if (other.CompareTag("Player") && !hasPlayed)
        {
            if (timelineDirector != null && timelineManager != null)
            {
                if (!timelineManager.IsTimelinePlaying(timelineID))
                {
                    timelineDirector.Play();
                    timelineManager.SetTimelineState(timelineID, true);
                    hasPlayed = true;
                    Debug.Log("TimelineTrigger: Player entered, timeline playing");

                    // 타임라인 종료 시 호출될 메서드 연결
                    timelineDirector.stopped += OnTimelineStopped;
                }
            }
        }
    }

    private void OnTimelineStopped(PlayableDirector pd)
    {
        if (timelineManager != null)
        {
            timelineManager.SetTimelineState(timelineID, false);
            Debug.Log("TimelineTrigger: Timeline stopped");
        }

        // 타임라인이 한 번 재생된 후 더 이상 실행되지 않도록 이벤트 해제
        timelineDirector.stopped -= OnTimelineStopped;
    }
}