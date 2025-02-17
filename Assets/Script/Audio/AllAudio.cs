using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllAudio : MonoBehaviour
{
    //メインメニューサウンド
    [Header("----------Audio Source----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource backSource;
    [SerializeField] AudioSource SFXSource;
    [SerializeField] AudioSource footSource;
    [SerializeField] AudioSource voiceSource;

    [Header("----------Audio Clip----------")]
    public AudioClip mainmenu;
    public AudioClip UIclick;
    public AudioClip UIonthemouse;
    public AudioClip firesound;
    public AudioClip enemyfiresound;
    public AudioClip emptygun;
    public AudioClip reloading;
    public AudioClip TtbotBGM;
    public AudioClip TtbotVoice;
    public AudioClip footstep;



    private void Start()
    {
        SFXSource.ignoreListenerPause = true;
    }

    public void PlayBGM(AudioClip clip)
    {
        musicSource.PlayOneShot(clip);
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void PlayBack(AudioClip clip)
    {
        backSource.PlayOneShot(clip);
    }

    public void PlayFoot(AudioClip clip)
    {
        footSource.PlayOneShot(clip);
    }

    public void PlayVoice(AudioClip clip)
    {
        voiceSource.PlayOneShot(clip);
    }

}
