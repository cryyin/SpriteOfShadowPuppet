using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bird : MonoBehaviour
{
    public GameObject button;
    public GameObject talkUI;
    public Animator anim;
    public int choiceNumber;

    private void Start()
    {

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
        anim.SetBool("fly", true);
    }
}
