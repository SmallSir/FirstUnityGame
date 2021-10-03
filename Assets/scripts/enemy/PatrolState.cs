using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PatrolState : EnemyBaseState 
{
	public override void EnterState(Enemy enemy)
	{
		enemy.animState = 0;
		enemy.SwitchPoint();
	}

	public override void OnUpdate(Enemy enemy)
	{
		if(Mathf.Abs(enemy.targetPoint.position.x - enemy.transform.position.x) < 0.01f)
			enemy.SwitchPoint();
		
		// 检测当前动画使用的是否是idle
		if(!enemy.anim.GetCurrentAnimatorStateInfo(0).IsName("idle"))
		{
			enemy.animState = 1;
		}
		enemy.MoveToTarget();
	}
}
