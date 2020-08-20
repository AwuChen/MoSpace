﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Link : MonoBehaviour
{

    bool runOnce = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        CheckInput();
    }

    void CheckInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition); RaycastHit mouseHit;

            if (Physics.Raycast(mouseRay, out mouseHit))
            {
                if (mouseHit.transform.GetComponent<Link>() != null)
                {
                    if(!runOnce)
                    {
                        StartCoroutine(OpenLink());
                    }
                }
                //else if (mouseHit.transform.GetComponent<Walkable>(). != null)
            }
        }
    }

    IEnumerator OpenLink()
    {
        //yield on a new YieldInstruction that waits for 3 seconds.
        yield return new WaitForSeconds(2);

        Application.OpenURL("https://youtu.be/dtla_S8lXIc");
        runOnce = true;
    }
}