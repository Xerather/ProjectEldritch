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
}
