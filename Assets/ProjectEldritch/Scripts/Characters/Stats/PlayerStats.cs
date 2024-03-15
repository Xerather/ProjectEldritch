using System;

[Serializable]
public class PlayerStats : BaseStats
{
	public const float DASH_DURATION = .5f;
	public float dashCooldown;
	public float dashSpeed;
	public float maxDashCounter;
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
		dashCooldown = stats.dashCooldown;
		maxDashCounter = stats.maxDashCounter;
		inventorySize = stats.inventorySize;
	}
}
