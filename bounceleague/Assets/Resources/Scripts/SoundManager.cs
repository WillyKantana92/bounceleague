using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SFXEnum
{
    BGM_01,
    
    PlayerHit, Goal
}

[Serializable]
public class AudioEntry
{
    public SFXEnum audioType;
    public AudioClip audioClip;
}


public class SoundManager : MonoBehaviour
{
    public AudioSource bgm;
    public AudioSource sfx;
    public List<AudioEntry> audioList;
    Dictionary<SFXEnum, AudioClip> audioDict;
    
    public void Initialize()
    {
        audioDict = new Dictionary<SFXEnum, AudioClip>();
        foreach(AudioEntry audioEntry in audioList)
        {
            audioDict.Add(audioEntry.audioType, audioEntry.audioClip);
        }
    }

    public void PlayBGM()
    {
        AudioClip ac = audioDict[SFXEnum.BGM_01];
        if(bgm.clip != ac)
        {
            bgm.clip = ac;
            bgm.Play();
        }
    }

    public void PlaySfx(SFXEnum soundEnum)
    {
        sfx.PlayOneShot(audioDict[soundEnum]);
    }
}