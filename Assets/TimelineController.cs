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

    private void Awake()
    {
        director = GetComponent<PlayableDirector>();
        playerAction = FindObjectOfType<PlayerAction>();

        Debug.Log("TimelineController: Awake - Director and PlayerAction initialized");
    }

    private void OnEnable()
    {
        Debug.Log("TimelineController: OnEnable - Events connected");
    }

    private void OnDisable()
    {
        Debug.Log("TimelineController: OnDisable - Events disconnected");
    }

    private void Update()
    {
        if (director.state == PlayState.Playing)
        {

            if (!wasPlaying)
            {
                wasPlaying = true;
                if (playerAction != null)
                {
                    playerAction.DisableInput();
                }
                Debug.Log("TimelineController: Timeline started - Input disabled");
            }
        }
        else
        {
            if (wasPlaying)
            {
                wasPlaying = false;
                if (playerAction != null)
                {
                    playerAction.EnableInput();
                }
                Debug.Log("TimelineController: Timeline stopped - Input enabled");
            }
        }
    }
}