using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.UI;

public class TextCommand : MonoBehaviour
{
    public GameObject[] spaces;
    public Text UserInput;
    // Update is called once per frame
    
    public void SubmitText()
    {
        for (int i = 0; i < spaces.Length; i++)
        {
            Debug.Log(UserInput.text + spaces[i].name);
            if (UserInput.text == spaces[i].name)
            {
                
                if (spaces[i].activeSelf)
                {
                    spaces[i].SetActive(false);
                }else
                {
                    spaces[i].SetActive(true);
                }
            }
        }
    }
}
