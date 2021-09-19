using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D rb; // 获取角色的刚体信息变量
    public float speed; // 横向移动速度
    public float jumpForcs; // 竖向(跳起)的力

    [Header("Ground Check")]
    public Transform groundCheck;
    public float checkRadius; // 检测范围
    public LayerMask  groundLayer; // 检测图层

    [Header("States Check")]
    public bool isGround;
    public bool canjump; // 是否是用跳跃按键 public才能在界面展示，private不展示
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // 获取刚体信息
    }

    // Update is called once per frame
    void Update() //  每一帧执行一次
    {
        CheckInput();
    }

    public void FixedUpdate() // 固定时间执行一次，差不多一秒左右
    {
        PhysicsCheck();
        Movement();
        Jump();
        
    }

    void CheckInput() 
    {
        if(Input.GetButtonDown("Jump") && isGround)
        {
            canjump = true;
        }
    }
    // 因为不同设备每秒的帧数不同，update一般都是获取键盘上的输入, fixedupdate获取都是物理引擎的一些内容
    void Movement() // 用户的移动
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal"); // 值的范围包括了-1 ~ 1包括小数.raw则是不包括小数
        rb.velocity = new Vector2(horizontalInput*speed, rb.velocity.y);
        if (horizontalInput != 0)
        {
            transform.localScale = new  Vector3(horizontalInput, 1, 1);
        }
    }

    void Jump()
    {
        if(canjump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForcs);
            rb.gravityScale = 4;
            canjump = false;
        }
    }

    void PhysicsCheck()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
        if(isGround)
        {
            rb.gravityScale = 1;
        }
    }
}
