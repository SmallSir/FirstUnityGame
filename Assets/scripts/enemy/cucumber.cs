using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cucumber : Enemy
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
}
