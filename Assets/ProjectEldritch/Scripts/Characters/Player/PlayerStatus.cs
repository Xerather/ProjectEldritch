using UnityEngine;
using System;

[Serializable]
public class PlayerStatus
{
	public float maxHp;
	public float hp;
	public float maxSanity;
	public float sanity;
	public float moveSpeed;
	public int inventorySize;
	[Header("Light parameter")]
	public float onInnerRadius;
	public float onOuterRadius;
	public float offInnerRadius;
	public float offOuterRadius;

	public void AddStats(PowerUpSO powerUp)
	{
		AddStats(powerUp.additionalStatus);
	}

	public void AddStats(PlayerStatus status)
	{
		maxHp += status.maxHp;
		maxSanity += status.maxSanity;

		hp = MathF.Min(hp + status.hp, maxHp);
		sanity = MathF.Min(sanity + status.sanity, maxSanity);

		moveSpeed += status.moveSpeed;
		inventorySize += status.inventorySize;

		onInnerRadius += status.onInnerRadius;
		onOuterRadius += status.onOuterRadius;
		offInnerRadius += status.offInnerRadius;
		offOuterRadius += status.offOuterRadius;
	}
}
