using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


public class AudioManager : BaseManager<AudioManager>
{
    public Dictionary<string, AudioClip> Music = new Dictionary<string, AudioClip>();

    public Dictionary<string, AudioClip> Sounds = new Dictionary<string, AudioClip>();

    public AudioSource MusicPlayer;
    public AudioSource SoundsPlayer;

    public void PlayMusic(string clipName)
    {
        MusicPlayer.clip = Music[clipName];
        MusicPlayer.Play();
    }

    public void PlaySound(string clipName)
    {
        if ( SoundsPlayer.isPlaying ) return;
        SoundsPlayer.clip = Sounds[clipName];
        SoundsPlayer.Play();
    }

    public void Stop()
    {
        MusicPlayer.Stop();
        SoundsPlayer.Stop();
    }

    protected override void OnAwake() {}

    private void Start()
    {
        PlayMusic("SummerSong");
    }
}
