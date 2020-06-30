using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static RPGTalk;

public class Bird : MonoBehaviour
{
    public GameObject button;
    public GameObject talkUI;
    public Animator anim;
    public RPGTalk rpgtalk;
    public int choiceNumber;

    private void Start()
    {
        choiceNumber = 1;
        anim.SetBool("fly", false);
    }

    private void Update()
    {
        if (button.activeSelf && Input.GetKeyDown(KeyCode.R))
        {
            talkUI.SetActive(true);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        button.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        button.SetActive(false);
    }

    public void Fly()
    {
        if (choiceNumber == 1)
        {
            anim.SetBool("fly", true);
        }
        
    }

    public void Choose(int num)
    {
        choiceNumber = num;
    }
}
