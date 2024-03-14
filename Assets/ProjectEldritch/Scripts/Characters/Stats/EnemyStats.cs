using System;

[Serializable]
public class EnemyStats : BaseStats
{
	public float chasingSpeed;
	public float eatDuration;
	public float searchDuration;

	public EnemyStats(EnemyStats stats) : base(stats)
	{
		chasingSpeed = stats.chasingSpeed;
		eatDuration = stats.eatDuration;
		searchDuration = stats.searchDuration;
	}
}