using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : Characters
{
	[SerializeField] private EnemySO enemySO;
	[SerializeField] private FloatEventChannelSO onEnemyGotHit;
	[SerializeField] private GameObject assasinationNotif;
	public EnemyStats enemyStats;
	private EnemyMovement enemyMovement;
	public float hp => enemyStats.hp;
	public float maxHp => enemyStats.maxHp;
	public WeakSideCollider[] weakSideColliderList;
	void Awake()
	{
		enemyMovement = GetComponent<EnemyMovement>();
		enemyStats = new EnemyStats(enemySO.baseEnemyStats);

		enemyMovement?.Setup(enemyStats);
		weakSideColliderList = GetComponentsInChildren<WeakSideCollider>();
	}

	protected override void Start()
	{
		base.Start();
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

		// if (enemyStats.hp == enemyStats.maxHp) StartCoroutine(GetStunned());
		enemyStats.hp -= hitDamage;
		onEnemyGotHit.RaiseEvent(hitDamage, hitDamage);
		if (enemyStats.hp <= 0)
		{
			CharacterDie();
			return;
		}
		StartCoroutine(BlinkingRed());
	}

	public void GetAssasinated()
	{
		GetHit(enemyStats.hp, true);
	}

	public void NotifyCanBeAssasinate(bool isActive)
	{
		assasinationNotif.SetActive(isActive);
	}

	protected IEnumerator GetStunned()
	{
		rendererColorChanger.TurnBlue();
		yield return new WaitForSeconds(2f);
		rendererColorChanger.TurnDefault();
		enemyMovement.LookAtPlayerDirection();
	}
}