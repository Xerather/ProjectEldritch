using UnityEngine;
using System;

[Serializable]
public class PlayerStatus
{
	public int maxHp;
	public int hp;
	public float moveSpeed;
	public int inventorySize;
	[Header("Light parameter")]
	public float onInnerRadius;
	public float onOuterRadius;
	public float offInnerRadius;
	public float offOuterRadius;
}
