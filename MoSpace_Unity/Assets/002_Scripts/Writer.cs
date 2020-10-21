using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Writer : MonoBehaviour
{
    bool isWriter = false;
    public GameObject[] writerStuff;
    public GameObject[] collectiveStuff;
    public void IsWriter(bool isit)
    {
        isWriter = isit;
    }

    public void Submit()
    {
        if(isWriter)
        {
            for (int i = 0; i < writerStuff.Length; i++)
            {
                writerStuff[i].SetActive(true);
            }
        }else
        {
            for (int i = 0; i < collectiveStuff.Length; i++)
            {
                collectiveStuff[i].SetActive(true);
            }
        }
    }
}
