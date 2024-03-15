using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeart : MonoBehaviour
{
	[SerializeField] private Enemy enemy;
	private void OnTriggerEnter2D(Collider2D col)
	{
		Debug.Log($"col = {col.gameObject.name}");
		if (col.gameObject.CompareTag("PlayerAttack"))
		{
			PlayerAttack attack = col.GetComponent<PlayerAttack>();
			if (attack == null) return;

			if (attack.e_PlayerAttackType == E_PlayerAttackType.Slash) enemy.GetHit(attack.hitDamage);
		}
	}
}
