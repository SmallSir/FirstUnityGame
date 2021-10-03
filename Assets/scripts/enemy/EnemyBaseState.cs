public abstract class EnemyBaseState 
{
	// 进入的执行方法
	public abstract void EnterState(Enemy enemy);

	// 持续运行的方法
	public abstract void OnUpdate(Enemy enemy);
}
