using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public GameObject guideDialog;
    public GameObject seaDialog;
    public GameObject skipDialog;
    public Rigidbody2D rb;
    public Collider2D coll;
    public Animator anim;
    public Transform groundcheck;
    public LayerMask ground;
    public LayerMask boat;
    public LayerMask sea;
    public bool playerDown = true;

    public float speed, jumpForce;

    public bool isGround, isJump;

    private bool jump_pressed;
    private int jump_count;
    private bool guide;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("Scene1"))//只在场景一显示提示
        {
            guide = true;
            anim.SetBool("down", true);
        }
        
        rb=GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        jump_count = 1;
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && jump_count > 0)
        {
            jump_pressed = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(groundcheck.position, 0.1f, ground);
        
        if (skipDialog.activeSelf)
        {
            guideDialog.SetActive(false);
            seaDialog.SetActive(false);
        }
        else
        {
            if (guide)
            {
                guideDialog.SetActive(true);
                guide = false;
            }
            
            waitToGetUp();
            if (!anim.GetBool("down"))
            {
                playerMove();
                playerJump();
                switchAnim();
                boatInSeaDialog();
            }
              
        }
    }

    //移动
    void playerMove()
    {
        float facedir=Input.GetAxisRaw("Horizontal");
        

        //左右
        if (coll.IsTouchingLayers(ground)|| coll.IsTouchingLayers(boat))
        {
            rb.velocity = new Vector2(facedir * speed, rb.velocity.y);
            anim.SetFloat("walking", Mathf.Abs(facedir));
        }
        if (facedir != 0)
        {
            transform.localScale = new Vector3(facedir, 1, 1);
        }

        
    }

    void playerJump()
    {
        //跳
        if (isGround)
        {
            jump_count = 1;
            isJump = false;
        }
        if (jump_pressed && isGround)
        {
            isJump = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jump_count--;
            jump_pressed = false;
            anim.SetBool("jumping", true);
        }
        else if(jump_pressed && jump_count > 0 && !isGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jump_count--;
            jump_pressed = false;
            anim.SetBool("jumping", true);
        }
        
    }

    void switchAnim()
    {
        anim.SetBool("idle", false);
        if (anim.GetBool("jumping"))
        {
            if (rb.velocity.y < 0)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }else if (coll.IsTouchingLayers(ground)|| coll.IsTouchingLayers(boat))
        {
            anim.SetBool("falling", false);
            anim.SetBool("idle", true);
        }

        if (coll.IsTouchingLayers(boat)&& coll.IsTouchingLayers(ground))
        {
            anim.SetBool("pushing", true);
        }
        else
        {
            anim.SetBool("pushing", false);
        }

    }

    void waitToGetUp()
    {
        if ((Input.GetKeyDown(KeyCode.Q) && playerDown)|| SceneManager.GetActiveScene().name.Equals("scene2"))//按了q或者在场景2，站起来
        {
            playerDown = false;
            anim.SetBool("down", false);
        }
    }

    void inSea()
    {
        if (coll.IsTouchingLayers(sea))
        {
            transform.position = new Vector2(-9.26f, 4.89f);
        }
    }

    void boatInSeaDialog()
    {
        if (coll.IsTouchingLayers(boat)) 
        {
            anim.SetBool("playerOnBoat", true);

            if (anim.GetBool("boatInSea"))
                seaDialog.SetActive(true);
            else
                seaDialog.SetActive(false);
        }
        else
        {
            anim.SetBool("playerOnBoat", false);
            seaDialog.SetActive(false);
        }
            
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name == "boat")
        {
            // Debug.Log("碰撞发生(角色脚本)");
            // Debug.Log(coll.contacts[0].normal.x);
            if (coll.contacts[0].normal.x == -1 || coll.contacts[0].normal.x == 1)
            {
                anim.SetBool("", false);
                anim.SetBool("", true);
            }
        }
        if (coll.gameObject.tag == "Sea")
        {
            Debug.Log("碰撞发生(角色脚本)");
            transform.position = new Vector3(-9.26f, 4.89f,0);
        }
    }

    private void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.name == "boat")
        {
            // Debug.Log("碰撞解除(角色脚本)");
            anim.SetBool("", false);
            anim.SetBool("", true);
        }
    }
}
