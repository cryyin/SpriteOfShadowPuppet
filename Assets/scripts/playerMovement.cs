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
    public LayerMask bird;
    public bool playerDown = true;

    public float speed, jumpForce;

    public bool isGround, isJump;

    private bool jump_pressed;
    private int jump_count;
    private bool guide;
    public float lastdir = 1;//之前人物朝向
    public bool play_re;
    public int questionAnswered=0;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("SampleScene"))//只在场景一显示提示
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
        

        anim.SetFloat("last_now", facedir * lastdir);
        if (facedir * lastdir < 0 )
        {
            anim.SetBool("turningleft", true);
            play_re = true;
        }
        // 判断动画是否播放完成
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.turnleft")&&anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            //播放完毕，要执行的内容
            anim.SetBool("turningleft", false);
            play_re = false;
        }
        if (facedir!=0)
        {
            lastdir = facedir;
        }

        //左右
        if (coll.IsTouchingLayers(ground) || coll.IsTouchingLayers(boat) || coll.IsTouchingLayers(bird))
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
        if (isGround || coll.IsTouchingLayers(bird))
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
        }else if (coll.IsTouchingLayers(ground)|| coll.IsTouchingLayers(boat) || coll.IsTouchingLayers(bird))
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

    private void OnTriggerEnter2D(Collider2D collision)//遭遇图二的鸟，开始对话
    {
        if (collision.tag == "QandAspace")
        {
            //触发对话
            questionAnswered++;
        }
    }
}


