using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineTrigger : MonoBehaviour
{
    public string timelineID; // Ÿ�Ӷ��� ���� ID
    public PlayableDirector timelineDirector; // ����� Ÿ�Ӷ���
    private TimelineManager timelineManager;
    private bool hasPlayed = false;

    private void Start()
    {
        timelineManager = FindObjectOfType<TimelineManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Ʈ���ſ� ���� ��ü�� "Player" �±׸� ���� ��쿡�� ����
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

                    // Ÿ�Ӷ��� ���� �� ȣ��� �޼��� ����
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

        // Ÿ�Ӷ����� �� �� ����� �� �� �̻� ������� �ʵ��� �̺�Ʈ ����
        timelineDirector.stopped -= OnTimelineStopped;
    }
}