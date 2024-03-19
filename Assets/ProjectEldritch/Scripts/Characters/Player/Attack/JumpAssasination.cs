using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAssasination : MonoBehaviour
{
	private Player player;
	[SerializeField] TooltipEventChannelSO interactionMessageChannel;
	[SerializeField] private List<Enemy> enemyList;
	public bool canAssasinate => enemyList.Count > 0;
	public Enemy closestEnemy;
	public void Setup(Player player)
	{
		this.player = player;
	}
	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag("Enemy") || col.CompareTag("EnemyHitBox"))
		{
			// interactionMessageChannel.RaiseEvent()
			Enemy enemy = col.GetComponentInParent<Enemy>();
			if (enemy.floorNumber >= player.floorNumber) return;
			if (enemyList.Contains(enemy)) return;
			enemyList.Add(enemy);

			CheckEnemyList();
		}
	}

	private void OnTriggerExit2D(Collider2D col)
	{
		if (col.CompareTag("Enemy") || col.CompareTag("EnemyHitBox"))
		{
			RemoveEnemyNotif();

			Enemy enemy = col.GetComponentInParent<Enemy>();
			enemyList.Remove(enemy);
			CheckEnemyList();
		}
	}

	private void CheckEnemyList()
	{
		GetClosestEnemy();
		NotifyEnemy();
	}

	public Enemy GetClosestEnemy()
	{
		float closest = 999;
		closestEnemy = null;

		foreach (Enemy enemy in enemyList)
		{
			float enemyDistance = CalculateDistance(enemy);
			if (enemyDistance >= closest) continue;
			closest = enemyDistance;
			closestEnemy = enemy;
		}
		return closestEnemy;
	}

	private void NotifyEnemy()
	{
		foreach (Enemy enemy in enemyList)
		{
			enemy.NotifyCanBeAssasinate(enemy == closestEnemy);
		}
	}

	public void RemoveEnemyNotif()
	{
		foreach (Enemy enemy in enemyList)
		{
			enemy.NotifyCanBeAssasinate(false);
		}
	}

	private float CalculateDistance(Enemy enemy)
	{
		return Vector3.Distance(player.transform.position, enemy.gameObject.transform.position);
	}
}
