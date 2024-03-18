using System;

[Serializable]
public class PlayerStats : BaseStats
{
	public float dashDuration;
	public float dashCooldown;
	public float dashSpeed;
	public float maxDashCounter;
	public int inventorySize;
	public float hitInvicibiltyCooldown;

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
		dashDuration = stats.dashDuration;
		dashCooldown = stats.dashCooldown;
		dashSpeed = stats.dashSpeed;
		maxDashCounter = stats.maxDashCounter;
		inventorySize = stats.inventorySize;
		hitInvicibiltyCooldown = stats.hitInvicibiltyCooldown;
	}
}
