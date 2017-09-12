using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 2017-9-12
/// huyy
/// 此类用于管理所有声音
/// </summary>
public class AudioManager : BaseManager {
    AudioSource bgAudioSource;
    AudioSource normalAudioSource;
    private readonly string PREFIX = "Sounds/";

    public AudioManager(GameFacade facade) : base(facade)
    {
        GameObject cameraObj = GameObject.FindGameObjectWithTag("MainCamera");
        bgAudioSource = cameraObj.AddComponent<AudioSource>();
        bgAudioSource.playOnAwake = false;
        bgAudioSource.loop = true;
        //bgAudioSource.
        normalAudioSource = cameraObj.AddComponent<AudioSource>();
        normalAudioSource.playOnAwake = false;
        PlayBg(SoundType.Bg_moderate);
    }

    public void PlayBg(SoundType soundtype,float volume =0.2f)
    {
        AudioClip clip = GetClip(soundtype);
        bgAudioSource.volume = volume;
        bgAudioSource.clip = clip;
        bgAudioSource.Play();
    }

    public void PlaySound(SoundType soundtype, float volume = 0.5f)
    {
        AudioClip clip = GetClip(soundtype);
        normalAudioSource.volume = volume;
        normalAudioSource.clip = clip;
        normalAudioSource.Play();
    }

    private AudioClip GetClip(SoundType soundtype)
    {
        string soundname = soundtype.ToString();
        string path = PREFIX + soundname;
        AudioClip clip = Resources.Load<AudioClip>(path);
        if(clip == null)
            throw new Exception("没有找到音源");
        return clip;
    }
}
