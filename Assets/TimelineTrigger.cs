using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineTrigger : MonoBehaviour
{
    public PlayableDirector timelineDirector; // ����� Ÿ�Ӷ���
    private bool hasPlayed = false; // Ÿ�Ӷ����� ����Ǿ����� ����

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasPlayed)
        {
            // �÷��̾ Ʈ���ſ� �����ϰ� Ÿ�Ӷ����� ���� ������� ���� ���
            if (timelineDirector != null)
            {
                timelineDirector.Play();
                hasPlayed = true; // Ÿ�Ӷ��� ��� ���� ������Ʈ
                Debug.Log("TimelineTrigger: Player entered, timeline playing");
            }
        }
    }
}