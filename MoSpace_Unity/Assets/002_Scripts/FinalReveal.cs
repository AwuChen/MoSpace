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
            
            winner.text = collectiveScore.ToString();
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
        for (int i = 0; i < finalVotes.Length; i++)
        {
            int ii = i; // ii = index of first number in subset
            int nn = 0; // nn = count of numbers in subset
                        // for each number in subset, count it
            for (; i < finalVotes.Length && finalVotes[i] == finalVotes[ii]; i++, nn++) { }
            // if the subset has more numbers than the best so far
            // remember it as the new best
            if (nBest < nn) { nBest = nn; iBest = ii; }
        }
        if (nBest != 1)
        {
            collectiveVote.text = finalVotes[iBest].text;
        }
        else
        {
            collectiveVote.text = "N/A";
        }

        // print the most popular value and how popular it is
        Debug.Log(finalVotes[iBest].text.ToString() + nBest);
    }
}
