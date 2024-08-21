using UnityEngine;
using UnityEngine.Playables;
using System.Collections.Generic;

public class TimelineManager : MonoBehaviour
{
    private Dictionary<string, bool> timelineStates = new Dictionary<string, bool>();

    // Ÿ�Ӷ��� ���� ����
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

    // Ÿ�Ӷ��� ���� Ȯ��
    public bool IsTimelinePlaying(string timelineID)
    {
        return timelineStates.ContainsKey(timelineID) && timelineStates[timelineID];
    }
}