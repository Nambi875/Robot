using UnityEngine;
using UnityEngine.Playables;

public class TriggerTimelineController : MonoBehaviour
{
    public PlayableDirector[] timelines; // ������ Ÿ�Ӷ��ε�
    private int currentTimelineIndex = 0;
    private TimelineManager timelineManager;

    private void Start()
    {
        timelineManager = FindObjectOfType<TimelineManager>();

        if (timelines.Length == 0 || timelineManager == null)
        {
            return;
        }

        foreach (var timeline in timelines)
        {
            timeline.stopped += OnTimelineStopped;
        }
    }

    // Ʈ���Ű� �߻��ϸ� ȣ��
    public void StartTimelineSequence()
    {
        if (timelines.Length > 0)
        {
            PlayCurrentTimeline();
        }
    }

    private void PlayCurrentTimeline()
    {
        if (currentTimelineIndex < timelines.Length)
        {
            var timeline = timelines[currentTimelineIndex];
            timeline.Play();
            timelineManager.SetTimelineState(timeline.name, true);
        }
    }

    private void OnTimelineStopped(PlayableDirector pd)
    {
        timelineManager.SetTimelineState(pd.name, false);
        currentTimelineIndex++;

        if (currentTimelineIndex < timelines.Length)
        {
            PlayCurrentTimeline(); // ���� Ÿ�Ӷ��� ���
        }
        else
        {
            Debug.Log("All timelines in this sequence have been played.");
        }
    }
}