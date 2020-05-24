using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float moveForce = 500f;
    public float maxSpeed = 5f;
    //  public float jumpForce = 100;
    public float jumpForce = 100;
    public bool jump = false;//判断角色是否跳起

    private bool grounded = false;
    private Transform groundCheck;
    private Rigidbody2D heroBody;
    public bool facingRight = true;//角色朝向

    private Animator anim;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();//获得控制权

        heroBody = GetComponent<Rigidbody2D>();
        groundCheck = transform.Find("GroundCheck");
        // BoxCollider2D bc2d = GetComponent<BoxCollider2D>();
    }
    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position
                                        , 1 << LayerMask.NameToLayer("Ground"));
        if (Input.GetButtonDown("Jump") && grounded)
            jump = true;
    }


    void FixedUpdate()
    {
        // 获取水平方向输入
        float h = Input.GetAxis("Horizontal");

        anim.SetFloat("Speed", Mathf.Abs(h));
        // 判断是否超过最大速度
        //if (h * heroBody.velocity.x < maxSpeed)
        if (h * GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * h * moveForce); //heroBody.AddForce(Vector2.right * h * moveForce);

        //if (Mathf.Abs(heroBody.velocity.x) > maxSpeed)
        if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed)
            GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y); 
        //heroBody.velocity = new Vector2(Mathf.Sign(heroBody.velocity.x) * maxSpeed,
        //-                                    heroBody.velocity.y);
        //
        

        if (h > 0 && !facingRight )
            Flip();

        if(h<0 && facingRight)
            Flip();
            
        if(jump)
        {
 
 //          heroBody.AddForce(new Vector2(0, jumpForce));
 //           jump = false;
            // 播放跳跃动画
            anim.SetTrigger("Jump");
            // 随机播放一个音效
 //           int i = Random.Range(0, jumpClips.Length);
  //          AudioSource.PlayClipAtPoint(jumpClips[i], transform.position);
            // 为角色跳跃增加向上的刚体力
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
            // 重置jump为false，以免为角色不断的增加向上刚体力
            jump = false;
        }
    }


    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
