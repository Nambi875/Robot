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

    // 마지막 위치와 회전을 저장하기 위한 변수
    private Vector3 lastPosition;
    private Quaternion lastRotation;

    private void Awake()
    {
        director = GetComponent<PlayableDirector>();
        playerAction = FindObjectOfType<PlayerAction>();

        // 디버그 메시지 추가
        Debug.Log("TimelineController: Awake - Director and PlayerAction initialized");
    }

    private void OnEnable()
    {
        // 디버그 메시지 추가
        Debug.Log("TimelineController: OnEnable - Events connected");
    }

    private void OnDisable()
    {
        // 디버그 메시지 추가
        Debug.Log("TimelineController: OnDisable - Events disconnected");
    }

    private void Update()
    {
        // 디버그 메시지로 재생 상태 확인
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

            // 현재 위치와 회전을 지속적으로 업데이트
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

                    // Timeline이 끝난 후 마지막 위치와 회전으로 플레이어를 설정
                    playerAction.transform.position = lastPosition;
                    playerAction.transform.rotation = lastRotation;

                    Debug.Log("TimelineController: Timeline stopped - Input enabled and position/rotation restored");
                }
            }
        }
    }
}