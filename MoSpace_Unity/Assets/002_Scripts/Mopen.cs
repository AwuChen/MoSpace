using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Mopen : MonoBehaviour
{
    string userName;
    bool runOnce = false;
    public bool pri = false;
    public bool pub = true;
    public bool per = false;
    public bool cin = false;

    public GameObject[] privateSpace;
    public GameObject[] publicSpace;
    public GameObject[] personalSpace;
    public GameObject cinemaSpace;

    void Start()
    {
        
    }

    private void CheckAccess()
    {

        print("checkAccess");
        //only active for awu & friends 
        if (userName == "Awu" || userName == "awu" || userName == "friend")
        {
            print("checkAccess2");
            for (int i = 0; i < personalSpace.Length; i++)
            {
                personalSpace[i].SetActive(true);
                print("checkAccess3");
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        // get local player name and check access 
        if ( !runOnce && other.tag == "LocalPlayer")
        {
            userName = other.gameObject.GetComponentInChildren<TextMesh>().text;
            print(userName);
            CheckAccess();
            runOnce = true;
        }
        //private
        if (pri)
        {
            if (other.tag == "NetworkPlayer")
            {
                other.gameObject.GetComponent<PlayerManager>().HidePlayer(true);
            }
        }
        //personal
        if (per)
        {
            //not awu but see awu and awu friend 
            if (userName != "Awu" && userName != "awu" && userName != "friend")
            {
                other.gameObject.GetComponent<PlayerManager>().HidePlayer(true);
            }
            else
            {
                other.gameObject.GetComponent<PlayerManager>().HidePlayer(false);
            }
        }
        //public
        if (pub)
        {
            if (other.tag == "NetworkPlayer")
            {
                other.gameObject.GetComponent<PlayerManager>().HidePlayer(false);
            }
        }

        if(cin)
        {
            if (other.tag == "LocalPlayer")
            {
                if(cinemaSpace.activeSelf)
                {
                    cinemaSpace.SetActive(false);
                    publicSpace[0].SetActive(true);
                }
                else
                {
                    cinemaSpace.SetActive(true);
                    publicSpace[0].SetActive(false);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
        //private 
        if (pri)
        {
            if (other.tag == "NetworkPlayer")
            {
                other.gameObject.GetComponent<PlayerManager>().HidePlayer(false);
            }
        }
        //personal
        if (per)
        {
            //not awu but see awu and awu friend 
            if (userName != "Awu" && userName != "awu" && userName != "friend")
            {
                other.gameObject.GetComponent<PlayerManager>().HidePlayer(false);
            }
        }

    }

}



