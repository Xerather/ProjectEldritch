using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : Characters
{
	[SerializeField] private EnemySO enemySO;
	[SerializeField] private FloatEventChannelSO onEnemyGotHit;
	private RendererColorChanger rendererColorChanger;
	public EnemyStats enemyStats;
	private EnemyMovement enemyMovement;
	public float hp => enemyStats.hp;
	public float maxHp => enemyStats.maxHp;
	public WeakSideCollider[] weakSideColliderList;
	void Awake()
	{
		enemyMovement = GetComponent<EnemyMovement>();
		enemyStats = new EnemyStats(enemySO.baseEnemyStats);
		rendererColorChanger = GetComponentInChildren<RendererColorChanger>();

		enemyMovement?.Setup(enemyStats);
		weakSideColliderList = GetComponentsInChildren<WeakSideCollider>();
	}

	private bool CheckCanBeHit()
	{
		for (int i = 0; i < weakSideColliderList.Length; i++)
		{
			if (weakSideColliderList[i].canBeHit) return true;
		}
		return false;
	}

	public void GetHit(float hitDamage, bool alwaysHit = false)
	{
		bool hitable = CheckCanBeHit() || alwaysHit;
		if (!hitable) return;

		enemyStats.hp -= hitDamage;
		onEnemyGotHit.RaiseEvent(hitDamage, hitDamage);
		if (enemyStats.hp <= 0)
		{
			CharacterDie();
			return;
		}
		StartCoroutine(EnemiesRed());
	}

	public void GetAssasinated()
	{
		GetHit(enemyStats.hp, true);
	}

	private IEnumerator EnemiesRed()
	{
		rendererColorChanger.TurnRed();
		yield return new WaitForSeconds(.3f);
		rendererColorChanger.TurnDefault();
	}
}