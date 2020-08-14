using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class MoveCamera : MonoBehaviour {
    [DllImport("__Internal")]
    private static extern bool IsMobile();
    public bool isMobile()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
         return IsMobile();
#endif
        return false;
    }

    public Vector3[] Positions;

    public int mCurrentIndex = 1;

    public float Speed = 2.0f;

    private void Start()
    {
        if (isMobile())
        {
            transform.position = new Vector3(15f, 22f, -25f);
        }
        else
        {
            transform.position = new Vector3(20f, 22f, -20f);
        }
    }
    // Update is called once per frame
    void Update () {

        //Vector3 currentPos = Positions[mCurrentIndex];

        //if (Input.GetKeyUp(KeyCode.RightArrow))
        //{
        //    if (mCurrentIndex < Positions.Length -1)
        //        mCurrentIndex++;
        //}

        //if(Input.GetKeyDown(KeyCode.LeftArrow))
        //{
        //    if (mCurrentIndex > 0)
        //        mCurrentIndex--;
        //}

        //transform.position = Vector3.Lerp(transform.position, currentPos, Speed * Time.deltaTime);

    }

    public void MoveRight()
    {
        if (isMobile())
        {
            Vector3 currentPos = Positions[mCurrentIndex];
            if (mCurrentIndex < Positions.Length - 1)
                mCurrentIndex++;
            transform.position = Vector3.Lerp(transform.position, currentPos, Speed * Time.deltaTime);
        }
    }

    public void MoveLeft()
    {
        if (isMobile())
        {
            Vector3 currentPos = Positions[mCurrentIndex];
            if (mCurrentIndex > 0)
                mCurrentIndex--;
            transform.position = Vector3.Lerp(transform.position, currentPos, Speed * Time.deltaTime);
        }
    }
}
