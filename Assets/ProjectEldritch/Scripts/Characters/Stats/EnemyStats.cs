using System;

[Serializable]
public class EnemyStats : BaseStats
{
	public float eatDuration;
	public float searchDuration;

	public EnemyStats(EnemyStats stats) : base(stats)
	{
		eatDuration = stats.eatDuration;
		searchDuration = stats.searchDuration;
	}
}