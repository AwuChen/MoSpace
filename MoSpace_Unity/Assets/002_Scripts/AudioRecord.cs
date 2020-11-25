using System;
using System.IO;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.Collections;

public class AudioRecord : MonoBehaviour
{
    int recordingCount = 0;
    int playCount = 0;
    AudioClip myAudioClip;
    public AudioSource myAudioSource;

    void Start() {
        
    }
    void Update() { }

    public void OnRecord()
    {
        //myAudioClip = Microphone.Start(null, false, 10, 44100);
    }

    public void OnSave()
    {
        SavWav.Save("Recording " + recordingCount.ToString(), myAudioClip);
        Debug.Log("Recording saved");
        recordingCount++;
    }

    public void OnPrevious()
    {
        if (playCount > 0)
        {
            playCount--;
            OnPlay(playCount);
        }

    }

    public void OnNext()
    {
        if(playCount < recordingCount)
        {
            playCount++;
            OnPlay(playCount);
        }
    }

    public void OnPlay(int count)
    {
        StartCoroutine(GetAudioClip(count));
    }

    IEnumerator GetAudioClip(int count)
    {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(Application.dataPath + "/Recording " + count.ToString() + ".wav", AudioType.WAV))
        {
            yield return www.SendWebRequest();

            if (www.error != null)
            {
                Debug.Log(www.error);
            }
            else
            {
                AudioClip myClip = DownloadHandlerAudioClip.GetContent(www);

                myAudioSource.clip = myClip;
                myAudioSource.Play();
            }
        }
    }
}