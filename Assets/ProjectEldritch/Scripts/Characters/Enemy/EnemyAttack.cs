using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
	public float hitDamage;
	public void Setup(PlayerStats stats)
	{
		hitDamage = stats.atk;
	}

	protected void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.CompareTag("PlayerHitBox"))
		{
			Player player = col.GetComponent<Player>();
			if (player == null) return;

			player.GetHit(hitDamage);
		}
	}
}
