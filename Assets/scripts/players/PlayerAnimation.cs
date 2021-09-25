using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
	// Start is called before the first frame update
	private Animator anim;
	private Rigidbody2D rb;
    private PlayerControl controller;
	void Start()
	{
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<PlayerControl>();
	}

	// Update is called once per frame
	void Update()
	{
		anim.SetFloat("speed", Mathf.Abs(rb.velocity.x)); // 获取横向速度的绝对值
        anim.SetBool("ground", controller.isGround);
        anim.SetBool("jump", controller.isJump);
        anim.SetFloat("velocityY", rb.velocity.y);
	}
}
