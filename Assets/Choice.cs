using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RPGTalk;
public class Choice : MonoBehaviour
{
    public string questionID;
    public int choiceID;
    public RPGTalk rpgtalk;
    // Start is called before the first frame update
    void Start()
    {
        rpgtalk.OnMadeChoice += OnMadeChoice;
    }

    private void Update()
    {
        
    }
    void OnMadeChoice(string questionID, int choiceID)
    {
        Debug.Log("Aha! In the question " + questionID + " you choosed the option " + choiceID);
    }
}
