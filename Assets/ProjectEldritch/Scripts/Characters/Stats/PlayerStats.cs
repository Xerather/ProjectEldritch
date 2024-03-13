using System;

[Serializable]
public class PlayerStats : BaseStats
{
	public int inventorySize;

	public void AddStats(PowerUpSO powerUp)
	{
		AddStats(powerUp.additionalStats);
	}

	public void AddStats(PlayerStats stats)
	{
		maxHp += stats.maxHp;

		hp = MathF.Min(hp + stats.hp, maxHp);

		speed += stats.speed;
		inventorySize += stats.inventorySize;
	}

	public PlayerStats(PlayerStats stats) : base(stats)
	{

	}
}
