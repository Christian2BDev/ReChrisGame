using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private List<AudioSource> audioSources = new List<AudioSource>();

    [SerializeField] AudioSource source;

    [SerializeField] AudioClip beginWave;
    [SerializeField] AudioClip explosionOne;
    [SerializeField] AudioClip explosionTwo;
    [SerializeField] AudioClip explosionMissWater;
    [SerializeField] AudioClip explosionMissLand;
    [SerializeField] AudioClip sinkingBoat;

    private void Awake()
    {
        for(int i = 0; i < 40; i++)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            audioSources.Add(source);
        }
    }

    public void PlayRandomExplosion()
    {
        if(Random.Range(0, 2) == 0)
        {
            PlaySound(explosionOne);
        }
        else
        {
            PlaySound(explosionTwo);
        }
    }

    public void PlayMissedBall(bool inWater)
    {
        if (inWater)
        {
            PlaySound(explosionMissWater);
        }
        else
        {
            PlaySound(explosionMissLand);
        }
    }

    public void PlayBeginWafe()
    {
        PlaySound(beginWave);
    }

    public void PlayBoatSink()
    {
        PlaySound(sinkingBoat);
    }

    void PlaySound(AudioClip clip)
    {
        AudioSource source = AvailableSource();
        if (source == null)
        {
            return;
        }
        source.clip = clip;
        source.Play();
    }

    AudioSource AvailableSource()
    {
        foreach (AudioSource source in audioSources)
        {
            if (!source.isPlaying)
            {
                return source;
            }
        }
        return null;
    }
}
