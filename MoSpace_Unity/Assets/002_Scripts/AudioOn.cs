using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioOn : MonoBehaviour
{
    public AudioSource aud;
    public AudioSource music;
    public bool musicOn = false;
    private void Update()
    {
        if (music.isPlaying)
        {
            aud.volume = 0.15f;
        }
        else aud.volume = 0.5f;
    }
    public void AudioToggle(bool state)
    {
        aud.mute = state;
    }
}
