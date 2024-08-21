using UnityEngine;
using UnityEngine.Playables;
using System.Collections.Generic;

public class TimelineManager : MonoBehaviour
{
    private Dictionary<string, bool> timelineStates = new Dictionary<string, bool>();

    // 타임라인 상태 관리
    public void SetTimelineState(string timelineID, bool isPlaying)
    {
        if (timelineStates.ContainsKey(timelineID))
        {
            timelineStates[timelineID] = isPlaying;
        }
        else
        {
            timelineStates.Add(timelineID, isPlaying);
        }
    }

    // 타임라인 상태 확인
    public bool IsTimelinePlaying(string timelineID)
    {
        return timelineStates.ContainsKey(timelineID) && timelineStates[timelineID];
    }
}