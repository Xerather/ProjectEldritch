using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
	[SerializeField] private Player player;
	private void OnTriggerStay2D(Collider2D col)
	{
		if (col.gameObject.CompareTag("EnemyAttack"))
		{
			Enemy enemy = col.GetComponentInParent<Enemy>();
			if (enemy.floorNumber != player.floorNumber) return;
			player.GetHit(1);
		}
	}
}
