using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : EnemyBaseState
{
    // Start is called before the first frame update
    public override void EnterState(Enemy enemy)
	{
		enemy.animState = 2;
		enemy.targetPoint = enemy.attackList[0];
	}

	public override void OnUpdate(Enemy enemy)
	{
		if (enemy.attackList.Count == 0)
		{
			enemy.TransitionToState(enemy.patrolState);
		}
		if (enemy.attackList.Count >= 1)
		{
			float min = enemy.targetPoint.position.x - enemy.transform.position.x;
			int index;
			for(int i = 0; i < enemy.attackList.Count; i++)
			{
				if(Mathf.Abs(enemy.transform.position.x - enemy.attackList[i].position.x) < min)
				{
					index = i;
					min = Mathf.Abs(enemy.transform.position.x - enemy.attackList[i].position.x);
				}
			}
		}

		if(enemy.targetPoint.CompareTag("Player"))
			enemy.AttackAction();
		else if(enemy.targetPoint.CompareTag("bomb"))
			enemy.SkillAction();
		enemy.MoveToTarget();
	}
}
