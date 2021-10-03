using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Movement")]
	public float speed;
	public Transform PointA;
	public Transform PointB;
	public Transform targetPoint;
	public List<Transform> attackList = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        SwitchPoint();
    }

    // Update is called once per frame
    void Update()
    {
		if(Mathf.Abs(targetPoint.position.x - transform.position.x) < 0.01f)
			SwitchPoint();
        MoveToTarget();
    }

	public void MoveToTarget() // 移动至目标
	{
		transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);
		FilpDirection();
	}

	public void AttackAction() // 攻击玩家、或者炸弹
	{

	}

	public void SkillAction() // 技能攻击
	{

	}

	public void FilpDirection() // 反转
	{
		if(targetPoint.position.x > transform.position.x)
			transform.rotation = Quaternion.Euler(0f, 0f, 0f);
		else
			transform.rotation = Quaternion.Euler(0f, 180f, 0f);
	}

	public void SwitchPoint()
	{
		if(Mathf.Abs(PointA.position.x - transform.position.x) > Mathf.Abs(PointB.position.x - transform.position.x))
			targetPoint = PointA;
		else
			targetPoint = PointB;
	}

	public void OnTriggerStay2D(Collider2D other) {
		if(!attackList.Contains(other.transform))
			attackList.Add(other.transform);
	}

	private void OnTriggerExit2D(Collider2D other) {
		attackList.Remove(other.transform);
	}
}
