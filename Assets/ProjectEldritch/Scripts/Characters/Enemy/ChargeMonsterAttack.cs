using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ChargeMonsterAttack : MonoBehaviour
{
	[SerializeField] private float cooldown;
	[SerializeField] private float attackDistance;
	[SerializeField] private float chargeDuration;
	[SerializeField] private float chargeSpeed;
	[SerializeField] private float chargePrepTime;
	[SerializeField] private float cooldownCounter;
	private Player player;
	private EnemyMovement enemyMovement;
	private Rigidbody2D rb;
	private FOVMechanic fov;
	void Awake()
	{
		cooldownCounter = 0;
		player = FindObjectOfType<Player>();
		rb = GetComponent<Rigidbody2D>();
		enemyMovement = GetComponent<EnemyMovement>();
		fov = GetComponentInChildren<FOVMechanic>();
	}

	void Update()
	{
		if (fov._spottedPlayer != null) DoCharge();
	}

	private bool PlayerInAttackRange()
	{
		if (player == null) return false;

		float playerDistance = Vector3.Distance(transform.position, player.transform.position);
		return playerDistance <= attackDistance;
	}
	private void DoCharge()
	{
		if (cooldownCounter <= 0 && PlayerInAttackRange())
		{
			enemyMovement.DoAttack();
			StartCoroutine(ChargeAnimation());
		}

		if (cooldownCounter > 0) cooldownCounter -= Time.deltaTime;

	}

	private IEnumerator ChargeAnimation()
	{
		yield return new WaitForSeconds(chargePrepTime);
		enemyMovement.LockRotation(true);
		rb.velocity = enemyMovement.GetPlayerDirection() * chargeSpeed;

		yield return new WaitForSeconds(chargeDuration);
		enemyMovement.LockRotation(false);
		rb.velocity = Vector2.zero;
		cooldownCounter = cooldown;
		enemyMovement.EndAttack();
	}
}
