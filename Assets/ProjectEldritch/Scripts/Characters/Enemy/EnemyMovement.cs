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
	[SerializeField] private Transform faceDirection;


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
		if (enemyState == E_EnemyState.Eating)
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
		Debug.Log($"Patrol started");

		enemyState = E_EnemyState.Patrol;

		patrol.enabled = true;
		aiDestinationSetter.enabled = false;
		fOVMechanic.enabled = true;
	}

	private void DoChase()
	{
		Debug.Log($"Chasing started");

		enemyState = E_EnemyState.Chase;

		patrol.enabled = false;
		aiDestinationSetter.enabled = true;
	}

	private void DoEat()
	{
		Debug.Log($"eating started");


		enemyState = E_EnemyState.Eating;
		soundMaker?.PlaySfx("hit");

		patrol.enabled = false;
		aiDestinationSetter.enabled = false;

		// fOVMechanic.enabled = false;

		StopMovement();
	}

	private void DoSearch()
	{
		Debug.Log($"Searching started");

		enemyState = E_EnemyState.Search;
		searchElapsed = 0f;
		// aiDestinationSetter.target
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
		faceDirection.right = aIPath.desiredVelocity;
	}
}

public enum E_EnemyState
{
	Patrol = 0,
	Search = 1,
	Chase = 2,
	Eating = 3
}
