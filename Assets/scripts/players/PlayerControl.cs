using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour, IDamageable
{
	private Rigidbody2D rb; // 获取角色的刚体信息变量
	private Animator anim;
	public float speed; // 横向移动速度
	public float jumpForcs; // 竖向(跳起)的力
	[Header("Player State")]
	public float health;
	public bool isDead = false;
	[Header("Ground Check")]
	public Transform groundCheck;
	public float checkRadius; // 检测范围
	public LayerMask groundLayer; // 检测图层

	[Header("States Check")]
	public bool isGround;
	public bool isJump;
	public bool canjump; // 是否是用跳跃按键 public才能在界面展示，private不展示
						 // Start is called before the first frame update

	[Header("Jump FX")] // 查看特效情况
	public GameObject jumpFX;
	public GameObject landFX;

    [Header("Attack Settings")]
    public GameObject bombPrefab;
    public float nextAttack = 0; // 炸弹计时器
    public float attackRate; // 放下炸弹频率
	void Start()
	{
		rb = GetComponent<Rigidbody2D>(); // 获取刚体信息
		anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update() //  每一帧执行一次
	{
		anim.SetBool("dead", isDead);
		if(isDead)
			return;
		CheckInput();
	}

	public void FixedUpdate() // 固定时间执行一次，差不多一秒左右
	{
		if(isDead)
			rb.velocity = Vector2.zero;
		PhysicsCheck();
		Movement();
		Jump();

	}

	void CheckInput()
	{
		if (Input.GetButtonDown("Jump") && isGround)
		{
			canjump = true;
		}

        if(Input.GetKeyDown(KeyCode.J)) {
            Attack();
        }
	}
	// 因为不同设备每秒的帧数不同，update一般都是获取键盘上的输入, fixedupdate获取都是物理引擎的一些内容
	void Movement() // 用户的移动
	{
		float horizontalInput = Input.GetAxisRaw("Horizontal"); // 值的范围包括了-1 ~ 1包括小数.raw则是不包括小数
		rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
		if (horizontalInput != 0)
		{
			transform.localScale = new Vector3(horizontalInput, 1, 1);
		}
	}

	void Jump()
	{
		if (canjump)
		{
			isJump = true;
			jumpFX.SetActive(true);
			jumpFX.transform.position = transform.position + new Vector3(0, -0.45f, 0);
			rb.velocity = new Vector2(rb.velocity.x, jumpForcs);
			rb.gravityScale = 4;
			canjump = false;
		}
	}

	public void LandFx()
	{
		landFX.SetActive(true);
		landFX.transform.position = transform.position + new Vector3(0, -0.75f, 0);
	}
	void PhysicsCheck() // 物理检测
	{
		isGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
		if (isGround)
		{
			isJump = false;
			rb.gravityScale = 1; // 修改重力
		}
	}

    public void Attack()
    {
        if(Time.time > nextAttack)
        {
            // 生成一个炸弹实例
            Instantiate(bombPrefab, transform.position, bombPrefab.transform.rotation);
            // 充值
            nextAttack = Time.time + attackRate;
        }
    }

	public void GetHit(float damage)
	{
		if(!anim.GetCurrentAnimatorStateInfo(0).IsName("hit"))
		{
			health -= damage;
			health = Mathf.Max(health, 0);
			if(health == 0)
			{
				isDead = true;
			}
			anim.SetTrigger("hit");
		}
	}
}
