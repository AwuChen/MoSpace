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
    public Text initialVote;
    public Text finalVote;
    public Text coScore;
    public Text wrScore;
    int collectiveScore;
    int writerScore;
    
    // Start is called before the first frame update
    void Start()
    {
        
        Debug.Log("SUBJECT REVALED: " + nManager.subjectReveal);
        subjectReveal.text = nManager.subjectReveal;
        if (isWriter)
        {
            for (int i = 0; i < initialVotes.Length; i++)
            {
                initialVotes[i].text = nManager.initialVotes[i];
            }
            for (int i = 0; i < finalVotes.Length; i++)
            {
                finalVotes[i].text = nManager.finalVotes[i];
            }
            for (int i = 0; i < initialVotes.Length; i++)
            {
                if (subjectReveal.text != initialVotes[i].ToString())
                {
                    writerScore++;
                }
            }
            wrScore.text = writerScore.ToString();
            //nManager.UpdateWriterScore(writerScore);
        }
        else
        {
            initialVote.text = nManager.initialVote;
            finalVote.text = nManager.finalVote;
            if (subjectReveal.text == initialVote.text)
            {
                collectiveScore++;
            }
            else
            {
                collectiveScore--;
            }
            coScore.text = collectiveScore.ToString();
            //nManager.UpdateCollectiveScore(collectiveScore);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
