using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fly : MonoBehaviour
{

   // Start is called before the first frame update
    public float speed;
    //private Rigidbody2D bird;
    private GameObject[] array;
    private Rigidbody2D[] rigid=new Rigidbody2D[7];
    private bool run=false;
    void Start()
    {
        array= GameObject.FindGameObjectsWithTag("Bird");
        for(int i=0;i<7;++i)
        {
            rigid[i]=array[i].GetComponent<Rigidbody2D>();
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        move();
    }
    void move()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
           
           run=true;
            //transform.Translate()
        } 
        if(run)
        {
            Debug.Log("按F");
            for(int i=0;i<7;++i)
            {
                Debug.Log(i+" "+array[i].transform.position.y);
                if(array[i].transform.position.y>=i-5)
                {
                    Debug.Log("循环");
                    rigid[i].velocity=Vector2.zero;
                    Debug.Log(rigid[i].velocity.y);
                }
                else
                {
                    //Debug.Log("循环");
                    rigid[i].velocity=new Vector2(rigid[i].velocity.x,speed);
                    Debug.Log(rigid[i].velocity.y);
                }
                
            }         
        }
       
    }
}
