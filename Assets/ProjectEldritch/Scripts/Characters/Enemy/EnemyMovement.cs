using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyMovement : MonoBehaviour
{
	[SerializeField] private AIPath aIPath;
	[SerializeField] private SoundMaker soundMaker;
	[SerializeField] private FOVMechanic fOVMechanic;
	[SerializeField] private float seachRadius;

	private Patrol patrol;
	private AIDestinationSetter aiDestinationSetter;
	private float eatElapsed;
	private float searchElapsed;
	private EnemyStats enemyStats;
	private float eatDuration => enemyStats.eatDuration;
	private float searchDuration => enemyStats.searchDuration;
	private E_EnemyState enemyState;
	public Transform faceDirection;
	[Header("Default Direction")]
	[SerializeField] private bool isGuardingSpecificDirection;
	[SerializeField] private Transform guardDirection;
	private Vector3 lastDirection;
	[SerializeField] private bool lockRotation = false;
	void Awake()
	{
		enemyState = E_EnemyState.Patrol;
		patrol = GetComponent<Patrol>();
		aiDestinationSetter = GetComponent<AIDestinationSetter>();
	}

	public void Setup(EnemyStats enemyStats)
	{
		this.enemyStats = enemyStats;
		aIPath.maxSpeed = enemyStats.speed;
		DoPatrol();
	}

	private void Update()
	{
		if (enemyState == E_EnemyState.Attack)
		{
			eatElapsed += Time.deltaTime;
			if (eatElapsed > eatDuration)
			{
				OnEndEat();
			}
		}

		if (enemyState == E_EnemyState.Search)
		{
			searchElapsed += Time.deltaTime;
			if (aIPath.reachedDestination)
			{
				aiDestinationSetter.targetVector = PickRandomPoint();
			}
			if (searchElapsed > searchDuration)
			{
				OnEndSearch();
			}
		}

		if (enemyState == E_EnemyState.Patrol)
		{
			if (aIPath.desiredVelocity == Vector3.zero)
			{
				faceDirection.right = isGuardingSpecificDirection ? GetDirectionTowardObject(guardDirection.gameObject) : lastDirection;
				LockRotation(true);
			}
			else
			{
				lastDirection = aIPath.desiredVelocity;
				LockRotation(false);
			}
		}
	}

	private void FixedUpdate()
	{
		RotateDirection();
	}

	public void StopMovement()
	{
		// Debug.Log($"enemy {this.name} stopped moving");
	}

	private void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.CompareTag("Player"))
		{
			DoEat();
		}
	}

	public void OnSpotEnter()
	{
		DoChase();
	}

	public void OnSpotStay()
	{

	}

	public void OnSpotExit()
	{
		if (enemyState == E_EnemyState.Attack) return;
		DoSearch();
	}

	private void OnEndEat()
	{
		// Debug.Log($"eating finished");
		// isEating = false;
		eatElapsed = 0;

		DoPatrol();
	}

	public void DoPatrol()
	{
		// Debug.Log($"Patrol started");

		enemyState = E_EnemyState.Patrol;

		patrol.enabled = true;
		aiDestinationSetter.enabled = false;
		fOVMechanic.enabled = true;
	}

	private void DoChase()
	{
		// Debug.Log($"Chasing started");

		enemyState = E_EnemyState.Chase;

		LockRotation(false);
		patrol.enabled = false;
		aiDestinationSetter.enabled = true;
	}

	private void DoEat()
	{
		// Debug.Log($"eating started");

		enemyState = E_EnemyState.Attack;
		soundMaker?.PlaySfx("hit");

		LockRotation(false);
		patrol.enabled = false;
		aiDestinationSetter.enabled = false;

		// fOVMechanic.enabled = false;

		StopMovement();
	}

	public void DoAttack()
	{
		enemyState = E_EnemyState.Attack;

		LockRotation(false);
		patrol.enabled = false;
		aiDestinationSetter.enabled = false;
		aIPath.canMove = false;
	}

	public void EndAttack()
	{
		aIPath.canMove = true;
		DoChase();
	}

	private void DoSearch()
	{
		// Debug.Log($"Searching started");

		LockRotation(false);
		enemyState = E_EnemyState.Search;
		searchElapsed = 0f;
	}

	private void OnEndSearch()
	{
		DoPatrol();
	}

	private Vector3 PickRandomPoint()
	{
		var point = Random.insideUnitSphere * seachRadius;

		point.y = 0;
		point += transform.position;
		return point;
	}

	private void RotateDirection()
	{
		// Debug.Log($"aIPath.desiredVelocity = {aIPath.desiredVelocity}");
		if (!lockRotation) faceDirection.right = enemyState == E_EnemyState.Attack ? GetPlayerDirection() : aIPath.desiredVelocity;
	}

	public Vector2 GetPlayerDirection()
	{
		GameObject player = fOVMechanic._spottedPlayer;
		if (player == null) return Vector2.zero;
		// Debug.Log($"{player.transform.position} - {transform.position} = {(player.transform.position - transform.position).normalized}");
		return (player.transform.position - transform.position).normalized;
	}

	public Vector2 LookAtPlayerDirection()
	{
		Player player = FindObjectOfType<Player>();
		if (player == null) return Vector2.zero;
		return (player.transform.position - transform.position).normalized;
	}

	public Vector2 GetDirectionTowardObject(GameObject target)
	{
		// Debug.Log($"{target} - {transform.position} = {(target.transform.position - transform.position).normalized}");
		return (target.transform.position - transform.position).normalized;
	}

	public void LockRotation(bool isLocked)
	{
		lockRotation = isLocked;
	}
}

public enum E_EnemyState
{
	Patrol = 0,
	Search = 1,
	Chase = 2,
	Attack = 3
}
