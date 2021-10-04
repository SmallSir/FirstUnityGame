using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cucumber : Enemy, IDamageable
{
	public Rigidbody2D rb;
	public override void Init()
	{
		base.Init();
		rb = GetComponent<Rigidbody2D>();
	}

	public void SetBombOff()
	{
		targetPoint.GetComponent<bomb>()?.TurnOff();
	}

	public void GetHit(float damage)
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
