using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : Characters
{
	[SerializeField] private EnemySO enemySO;
	private EnemyMovement enemyMovement;
	private EnemyStats enemyStats;


	void Awake()
	{
		enemyMovement = GetComponent<EnemyMovement>();
		enemyStats = new EnemyStats(enemySO.baseEnemyStats);

		enemyMovement?.Setup(enemyStats);
	}
}