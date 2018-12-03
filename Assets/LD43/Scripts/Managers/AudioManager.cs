using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using VuLib;

public class AudioManager : Singleton<AudioManager>
{
    public AudioSource _sfx;
    public AudioSource _bgm;
    
    public float _defaultClickVolume = 1.0f;
    public AudioClip _confirmSFX;

    public AudioClip PlayOneShot(AudioClip[] clips, float volume = 1.0f)
    {
        if(clips.Length > 0) {
            AudioClip playedClip = clips[Random.Range(0, clips.Length)];
            PlayOneShot(playedClip, volume);
            return playedClip;
        }
        return null;
    }

    public AudioClip PlayOneShot(AudioClip clip, float volume = 1.0f)
    {
        if (clip != null)
        {
            _sfx.PlayOneShot(clip, volume);
        }

        return clip;
    }

    public void PlayDefaultClickSound()
    {
        PlayOneShot(_confirmSFX, _defaultClickVolume);
    }

    public void StopBGM()
    {
        _bgm.Stop();
    }
}
