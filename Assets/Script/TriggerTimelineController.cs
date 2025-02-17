using UnityEngine;
using UnityEngine.Playables;

public class TriggerTimelineController : MonoBehaviour
{
    public PlayableDirector[] timelines; // 관리할 타임라인들
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

    // 트리거가 발생하면 호출
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
            PlayCurrentTimeline(); // 다음 타임라인 재생
        }
        else
        {
            Debug.Log("All timelines in this sequence have been played.");
        }
    }
}