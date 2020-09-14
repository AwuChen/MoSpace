using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldUpdate : MonoBehaviour
{
    GameObject LP;
    private void Start()
    {

    }
    public void UpdateWorld(string obj)
    {
        if (LP == null)
        {
            LP = GameObject.Find("LocalPlayer");
        }
        if (LP != null)
        {
            LP.GetComponent<PlayerManager>().Interact(obj);
        }
    }
}
