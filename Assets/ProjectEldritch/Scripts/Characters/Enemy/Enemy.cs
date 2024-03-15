using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : Characters
{
	[SerializeField] private EnemySO enemySO;
	[SerializeField] private FloatEventChannelSO onEnemyGotHit;
	public EnemyStats enemyStats;
	private EnemyMovement enemyMovement;
	public float hp => enemyStats.hp;
	public float maxHp => enemyStats.maxHp;
	void Awake()
	{
		enemyMovement = GetComponent<EnemyMovement>();
		enemyStats = new EnemyStats(enemySO.baseEnemyStats);

		enemyMovement?.Setup(enemyStats);
	}

	public void GetHit(float hitDamage)
	{
		enemyStats.hp -= hitDamage;
		onEnemyGotHit.RaiseEvent(hitDamage, hitDamage);
		if (enemyStats.hp <= 0)
		{
			CharacterDie();
		}
	}

	public void GetAssasinated()
	{
		GetHit(enemyStats.hp);
	}
}