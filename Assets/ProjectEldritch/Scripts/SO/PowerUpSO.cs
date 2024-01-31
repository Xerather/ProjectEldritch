using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Data/PowerUp")]
public class PowerUpSO : ItemSO
{
	public float maxHp;
	public float hp;
	public float maxSanity;
	public float sanity;
	public float moveSpeed;
	public int inventorySize;
	public float outerRadiusRange;
	public float innerRadiusRange;
}
