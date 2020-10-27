using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Themes : MonoBehaviour
{
    bool isWriter = false;
    public GameObject[] themes;
    public int selection;
    public void IsWriter(bool isit)
    {
        isWriter = isit;
    }
    public void SetSelection(int num)
    {
        selection = num;
    }

    public void SelectTheme()
    {
        themes[selection].SetActive(true);
    }
}
