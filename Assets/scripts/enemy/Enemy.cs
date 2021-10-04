using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	EnemyBaseState currentState;
	public Animator anim;
	public int animState;

	[Header("Base State")]
	public float health;
	public bool isDead;
	// Start is called before the first frame update
	[Header("Movement")]
	public float speed;
	public Transform PointA;
	public Transform PointB;
	public Transform targetPoint;

	[Header("Attack Setting")]
	public float attackRate;
	private float nextAttack = 0;
	public float attackRange, skillRange;

	public List<Transform> attackList = new List<Transform>();
	// Start is called before the first frame update

	public PatrolState patrolState = new PatrolState();
	public AttackState attackState = new AttackState();

	public virtual void Init()
	{
		anim = GetComponent<Animator>();
	}

	void Awake()
	{
		Init();
	}
	void Start()
	{
		TransitionToState(patrolState);
	}

	// Update is called once per frame
	void Update()
	{
		anim.SetBool("isDead", isDead);
		if(isDead)
			return;
		currentState.OnUpdate(this);
		anim.SetInteger("state", animState);
	}

	public void MoveToTarget() // 移动至目标
	{
		transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);
		FilpDirection();
	}

	public void AttackAction() // 攻击玩家、或者炸弹
	{
		if(Vector2.Distance(transform.position, targetPoint.position) < attackRange)
		{
			if(Time.time > nextAttack)
			{
				anim.SetTrigger("attack");
				nextAttack = Time.time + attackRate;
			}
		}
	}

	public void SkillAction() // 技能攻击
	{
		if(Vector2.Distance(transform.position, targetPoint.position) < skillRange)
		{
			if(Time.time > nextAttack)
			{
				anim.SetTrigger("skill");
				// 播放攻击动画
				nextAttack = Time.time + attackRate;
			}
		}
	}

	public void FilpDirection() // 反转
	{
		if (targetPoint.position.x > transform.position.x)
			transform.rotation = Quaternion.Euler(0f, 0f, 0f);
		else
			transform.rotation = Quaternion.Euler(0f, 180f, 0f);
	}

	public void SwitchPoint()
	{
		if (Mathf.Abs(PointA.position.x - transform.position.x) > Mathf.Abs(PointB.position.x - transform.position.x))
			targetPoint = PointA;
		else
			targetPoint = PointB;
	}

	public void OnTriggerStay2D(Collider2D other)
	{
		if (!attackList.Contains(other.transform))
			attackList.Add(other.transform);
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		attackList.Remove(other.transform);
	}


	public void TransitionToState(EnemyBaseState state)
	{
		currentState = state;
		currentState.EnterState(this);
	}
}
