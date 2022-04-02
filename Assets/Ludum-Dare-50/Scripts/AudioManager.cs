using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


public class AudioManager : BaseManager<AudioManager>
{
    public Dictionary<string, AudioClip> Music = new Dictionary<string, AudioClip>();

    public AudioSource AudioSource { get; private set; }

    public void Play(string clipName)
    {
        AudioSource.clip = Music[clipName];
        AudioSource.Play();
    }

    public void Stop()
    {
        AudioSource.Stop();
    }

    protected override void OnAwake()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        Play("SummerSong");
    }
}
