using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatInSeaDialog : MonoBehaviour
{
    public GameObject skipDialog;
    public Animator anim;

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (!skipDialog.activeSelf && coll.gameObject.tag == "Sea")
        {
            anim.SetBool("boatInSea", true);
            anim.SetBool("pushing", false);
        }
    }

    private void OnCollisionExit2D(Collision2D coll)
    {
        if(!skipDialog.activeSelf && coll.gameObject.tag == "Sea")
        {
            anim.SetBool("boatInSea", false);
        }
    }
}
