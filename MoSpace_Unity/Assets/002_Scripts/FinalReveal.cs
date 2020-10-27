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
    public Text subjectVote;
    public Text individualVote;
    public Text collectiveVote;
    public Text winner;
    int collectiveScore;
    int writerScore;

    //Variable that defines comma character as separator
    static private readonly char[] Delimiter = new char[] { ',' };

    // Start is called before the first frame update
    void Start()
    {
        
        Debug.Log("SUBJECT REVALED: " + nManager.subjectReveal);
        subjectReveal.text = nManager.subjectReveal;
        // check what subject voted for 
        for (int i = 0; i < nManager.finalVotes.Length; i++)
        {
            if ((nManager.finalVotes[i].Split(Delimiter))[0].ToLower() == subjectReveal.text.ToString())
            {
                subjectVote.text = (nManager.finalVotes[i].Split(Delimiter))[1];
            }
            else
            {
                Debug.Log((nManager.finalVotes[i].Split(Delimiter))[0].ToLower() + subjectReveal.text.ToString());
            }
        }
        if (isWriter)
        {
            //for (int i = 0; i < initialVotes.Length; i++)
            //{
            //    initialVotes[i].text = nManager.initialVotes[i];
            //}
            for (int i = 0; i < finalVotes.Length; i++)
            {
                var pack = nManager.finalVotes[i].Split(Delimiter);
                finalVotes[i].text = pack[1];
            }

            // find the majority vote from nManager.finalVotes[i]
            FindMajorityVote();

            if (subjectReveal.text == collectiveVote.text)
            {
                if (subjectVote.text == subjectReveal.text)
                {
                    winner.text = "collective & subject\n(too easy)";
                }
                else
                {
                    winner.text = "collective";
                }

            }
            else if (subjectVote.text == subjectReveal.text)
            {
                winner.text = "writer & subject";
            }
            else
            {
                winner.text = "No one, GG\n(good guess)";
            }
            Debug.Log(subjectVote.text + subjectReveal.text);

        }
        else
        {
            individualVote.text = nManager.finalVote;
            
           
            // find the majority vote from nManager.finalVotes[i]
            FindMajorityVote();

            if (subjectReveal.text == collectiveVote.text)
            {
                if (subjectVote.text == subjectReveal.text)
                {
                    winner.text = "collective & subject\n(too easy)";
                }
                else
                {
                    winner.text = "collective";
                }
            }
            else if (subjectVote.text == subjectReveal.text)
            {
                winner.text = "writer & subject";
            }else
            {
                winner.text = "No one, GG\n(good guess)";
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
            for (; i < nManager.finalVotes.Length && (nManager.finalVotes[i].Split(Delimiter))[1] == (nManager.finalVotes[ii].Split(Delimiter))[1]; i++, nn++) { }
            // if the subset has more numbers than the best so far
            // remember it as the new best
            if (nBest < nn) { nBest = nn; iBest = ii; }
        }

        if(!IsArrayUnique())
        {
            collectiveVote.text = (nManager.finalVotes[iBest].Split(Delimiter))[1];
        }
        else
        {
            collectiveVote.text = "N/A";
        }

        // print the most popular value and how popular it is
        Debug.Log((nManager.finalVotes[iBest].Split(Delimiter))[1].ToString() + nBest);
    }

    public bool IsArrayUnique()
    {
        for (var i = 0; i < nManager.finalVotes.Length; i++)
        {
            for (var j = 0; j < nManager.finalVotes.Length; j++)
            {
                if (i != j)
                {
                    if ((nManager.finalVotes[i].Split(Delimiter))[1].ToString().Equals((nManager.finalVotes[j].Split(Delimiter))[1].ToString()))
                    {
                        return false; // means there are duplicate values
                    }
                }
            }
        }
        return true; // means there are no duplicate values.
    }
}
