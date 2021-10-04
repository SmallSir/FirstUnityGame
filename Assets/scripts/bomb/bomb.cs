using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : MonoBehaviour
{
	private Animator anim; // 爆炸动画对象
						   // Start is called before the first frame update
	private Collider2D coll;
    private Rigidbody2D rb;
    public float startTime; // 炸弹开始时间
	public float waitTime; // 炸弹结束时间
	public float bombForce; // 炸弹爆炸的力

	[Header("check")]
	public float radius; // 炸弹爆炸范围
	public LayerMask targetLayer; // 炸弹爆炸的图层
	void Start()
	{
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        startTime = Time.time;
	}

	// Update is called once per frame
	void Update()
	{
		if(!anim.GetCurrentAnimatorStateInfo(0).IsName("bomb_off") && Time.time > startTime + waitTime)
        {
            anim.Play("bomb_explotion"); // 播放指定的动画
        }
	}

	public void OnDrawGizmos() // 展示爆炸范围
	{
		Gizmos.DrawWireSphere(transform.position, radius);
	}

	// 爆炸时候检测所有的碰撞体, 并且对其施加力
	public void Explotion() // animation envent
	{
        // 避免炸弹爆炸后对自己作用力, 取消碰撞体
		coll.enabled = false;
		// 获取在范围内的所有物体
		// 物体的物理检测条件是物体要有刚体和碰撞体
		Collider2D[] aroundObjects = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);

		// 避免取消碰撞体碰撞后掉出地图, 将重力变为0;
		rb.gravityScale = 0;

		foreach (var item in aroundObjects)
		{
			Vector3 pos = transform.position - item.transform.position;

			// 给刚体添加一个反方向向上的力, 并且这个力类似推一下
			item.GetComponent<Rigidbody2D>().AddForce((-pos +Vector3.up) * bombForce, ForceMode2D.Impulse);
			// 如果爆炸时候周围也有炸弹,则将熄灭的炸弹进行点燃
			if(item.CompareTag("bomb") && item.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("bomb_off"))
			{
				item.GetComponent<bomb>().TurnOn();
			}
		}
        coll.enabled = true;
	}

    // 删除动画
    public void DestroyThis()
    {
        Destroy(gameObject);
    }

	public void TurnOff()
	{
		anim.Play("bomb_off");
		gameObject.layer = LayerMask.NameToLayer("npc");
	}

	public void TurnOn()
	{
		startTime = Time.time;
		anim.Play("bomb_on");
		gameObject.layer = LayerMask.NameToLayer("bomb");
	}
}
