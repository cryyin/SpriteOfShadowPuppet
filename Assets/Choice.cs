using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Choice : MonoBehaviour
{
    public static int choice = 999;
    public int choiceID;
    public Transform transform;

    private void Update()
    {
        choiceID = transform.GetSiblingIndex();
    }
    public void OnClick()
    {
        if (choiceID == 0)
        {
            choice = 0;
        }
    }
}
