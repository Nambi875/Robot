using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineController : MonoBehaviour
{
    private PlayableDirector director;
    private PlayerAction playerAction;
    private bool wasPlaying;

    // ������ ��ġ�� ȸ���� �����ϱ� ���� ����
    private Vector3 lastPosition;
    private Quaternion lastRotation;

    private void Awake()
    {
        director = GetComponent<PlayableDirector>();
        playerAction = FindObjectOfType<PlayerAction>();

        // ����� �޽��� �߰�
        Debug.Log("TimelineController: Awake - Director and PlayerAction initialized");
    }

    private void OnEnable()
    {
        // ����� �޽��� �߰�
        Debug.Log("TimelineController: OnEnable - Events connected");
    }

    private void OnDisable()
    {
        // ����� �޽��� �߰�
        Debug.Log("TimelineController: OnDisable - Events disconnected");
    }

    private void Update()
    {
        // ����� �޽����� ��� ���� Ȯ��
        if (director.state == PlayState.Playing)
        {
            Debug.Log("TimelineController: Director is playing");

            if (!wasPlaying)
            {
                wasPlaying = true;
                if (playerAction != null)
                {
                    playerAction.DisableInput();
                }
                Debug.Log("TimelineController: Timeline started - Input disabled");
            }

            // ���� ��ġ�� ȸ���� ���������� ������Ʈ
            lastPosition = playerAction.transform.position;
            lastRotation = playerAction.transform.rotation;
        }
        else
        {
            if (wasPlaying)
            {
                wasPlaying = false;
                if (playerAction != null)
                {
                    playerAction.EnableInput();

                    // Timeline�� ���� �� ������ ��ġ�� ȸ������ �÷��̾ ����
                    playerAction.transform.position = lastPosition;
                    playerAction.transform.rotation = lastRotation;

                    Debug.Log("TimelineController: Timeline stopped - Input enabled and position/rotation restored");
                }
            }
        }
    }
}