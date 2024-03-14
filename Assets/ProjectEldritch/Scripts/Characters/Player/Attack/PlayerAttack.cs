using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
	public float hitDamage;
	public E_PlayerAttackType e_PlayerAttackType;
	public void Setup(PlayerStats stats)
	{
		hitDamage = stats.atk;
	}

	protected void OnTriggerEnter2D(Collider2D col)
	{
		// if (col.gameObject.CompareTag("EnemiesWeakness"))
		// {
		// 	Enemy enemy = col.GetComponent<Enemy>();
		// 	if (enemy == null) return;

		// 	enemy.GetHit(hitDamage);
		// }
	}

	public void DestroySelf()
	{
		Destroy(gameObject);
	}
}

public enum E_PlayerAttackType
{
	Slash = 0,
	Shuriken = 1,
	SpecialProjectile = 2
}
