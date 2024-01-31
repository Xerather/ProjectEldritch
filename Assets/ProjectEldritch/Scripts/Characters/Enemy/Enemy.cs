using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : Characters
{
	[SerializeField] private EnemySO enemySO;
	[SerializeField] private EnemyMovement enemyMovement;
	[SerializeField] private PatrolMechanic patrolMechanic;
	[SerializeField] private AIDestinationSetter aiDestinationSetter;
	[SerializeField] private FOVMechanic fOVMechanic;
	[SerializeField] private SoundMaker soundMaker;
	private float eatDuration => enemySO.eatDuration;
	private float searchDuration => enemySO.searchDuration;
	private float eatElapsed;
	private bool isEating;
	private float searchElapsed;
	private bool isSearching;
	private void Update()
	{
		if (isEating)
		{
			eatElapsed += Time.deltaTime;
			if (eatElapsed > eatDuration)
			{
				OnEndEat();
			}
		}

		if (isSearching)
		{
			searchElapsed += Time.deltaTime;
			if (searchElapsed > searchDuration)
			{
				OnEndSearch();
			}
		}

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
		enemyMovement.PlayerSpottedEnter();
	}

	public void OnSpotStay()
	{

	}

	public void OnSpotExit()
	{
		DoSearch();
		enemyMovement.PlayerSpottedExit();
	}

	private void DoChase()
	{
		if (isEating) return;
		if (!isSearching) soundMaker.PlaySfx("growl");
		patrolMechanic.enabled = false;
		aiDestinationSetter.enabled = true;
	}

	private void DoEat()
	{

		// Debug.Log($"eating started for {eatDuration}s");
		isEating = true;
		soundMaker.PlaySfx("hit");
		patrolMechanic.enabled = false;
		fOVMechanic.enabled = false;

		aiDestinationSetter.enabled = false;
		enemyMovement.StopMovement();
	}

	private void OnEndEat()
	{
		// Debug.Log($"eating finished");
		eatElapsed = 0;
		isEating = false;
		patrolMechanic.enabled = true;
		fOVMechanic.enabled = true;

		enemyMovement.DoPatrol();
	}

	private void DoSearch()
	{
		// Debug.Log($"search started for {eatDuration}s");
		isSearching = true;
		searchElapsed = 0f;
	}

	private void OnEndSearch()
	{
		// Debug.Log($"search finished");
		if (isEating) return;
		isSearching = false;
		patrolMechanic.enabled = true;
		aiDestinationSetter.enabled = false;
		enemyMovement.DoPatrol();
	}
}
