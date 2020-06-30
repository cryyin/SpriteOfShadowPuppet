using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fly4 : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    private bool run = false;
    private Rigidbody2D bird;
    void Start()
    {
       
        bird=GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }
    void move()
    {
        if (Choice.choice == 0)
        {

            run = true;
            //transform.Translate()
        }
        if (run)
        {
            bird.velocity = new Vector2(bird.velocity.x, speed);
            Debug.Log(transform.position.y );
            if (transform.position.y >=-2)
            {
                bird.velocity = Vector2.zero;
            }
             
            
        }

    }
}
