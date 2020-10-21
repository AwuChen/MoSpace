using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.UI;

public class FinalReveal : MonoBehaviour
{
    public NetworkManager nManager;
    public bool isWriter = false;
    public Text subjectReveal;
    public Text[] initialVotes;
    public Text[] finalVotes;
    public Text individualVote;
    public Text collectiveVote;
    public Text winner;
    int collectiveScore;
    int writerScore;
    
    // Start is called before the first frame update
    void Start()
    {
        
        Debug.Log("SUBJECT REVALED: " + nManager.subjectReveal);
        subjectReveal.text = nManager.subjectReveal;
        if (isWriter)
        {
            //for (int i = 0; i < initialVotes.Length; i++)
            //{
            //    initialVotes[i].text = nManager.initialVotes[i];
            //}
            for (int i = 0; i < finalVotes.Length; i++)
            {
                finalVotes[i].text = nManager.finalVotes[i];
            }

            // find the majority vote from nManager.finalVotes[i]
            FindMajorityVote();

            if (subjectReveal.text == collectiveVote.text)
            {
                winner.text = "collective";
            }
            else
            {
                winner.text = "writer";
            }

            
        }
        else
        {
            individualVote.text = nManager.finalVote;
            
           
            // find the majority vote from nManager.finalVotes[i]
            FindMajorityVote();

            if (subjectReveal.text == collectiveVote.text)
            {
                winner.text = "collective";
            }
            else
            {
                winner.text = "writer";
            }
            
            //winner.text = collectiveScore.ToString();
            //nManager.UpdateCollectiveScore(collectiveScore);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FindMajorityVote()
    {
        // assuming n > 0
        int iBest = -1;  // index of first number in most popular subset
        int nBest = -1;  // popularity of most popular number
                         // for each subset of numbers
        for (int i = 0; i < nManager.finalVotes.Length;)
        {
            int ii = i; // ii = index of first number in subset
            int nn = 0; // nn = count of numbers in subset
                        // for each number in subset, count it
            for (; i < nManager.finalVotes.Length && nManager.finalVotes[i] == nManager.finalVotes[ii]; i++, nn++) { }
            // if the subset has more numbers than the best so far
            // remember it as the new best
            if (nBest < nn) { nBest = nn; iBest = ii; }
        }

        if(!IsArrayUnique())
        {
            collectiveVote.text = nManager.finalVotes[iBest];
        }
        else
        {
            collectiveVote.text = "N/A";
        }

        // print the most popular value and how popular it is
        Debug.Log(nManager.finalVotes[iBest].ToString() + nBest);
    }

    public bool IsArrayUnique()
    {
        for (var i = 0; i < nManager.finalVotes.Length; i++)
        {
            for (var j = 0; j < nManager.finalVotes.Length; j++)
            {
                if (i != j)
                {
                    if (nManager.finalVotes[i].ToString().Equals(nManager.finalVotes[j].ToString()))
                    {
                        return false; // means there are duplicate values
                    }
                }
            }
        }
        return true; // means there are no duplicate values.
    }
}
