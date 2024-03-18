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
		if (col.CompareTag("Enemy"))
		{
			// interactionMessageChannel.RaiseEvent()
			Enemy enemy = col.GetComponent<Enemy>();
			if (enemy.floorNumber >= player.floorNumber) return;
			enemyList.Add(enemy);

			Debug.Log($"GOT ENEMIES = {col.gameObject.name}");
			GetClosestEnemy();
			NotifyEnemy();
		}
	}

	private void OnTriggerExit2D(Collider2D col)
	{
		if (col.CompareTag("Enemy"))
		{
			Debug.Log($"removed = {col.gameObject.name}");
			GetClosestEnemy();
			NotifyEnemy();
			Enemy enemy = col.GetComponent<Enemy>();
			enemyList.Remove(enemy);
		}
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
			Debug.Log($"closest enemy == null ? {closestEnemy == null} || enemy == closestenemy? {enemy == closestEnemy}");
			enemy.NotifyCanBeAssasinate(enemy == closestEnemy);
		}
	}

	private float CalculateDistance(Enemy enemy)
	{
		return Vector3.Distance(player.transform.position, enemy.gameObject.transform.position);
	}
}
