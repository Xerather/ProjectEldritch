using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyMovement : MonoBehaviour
{
	[SerializeField] private AIPath aIPath;
	[SerializeField] private Vector2 directions;
	[SerializeField] private float searchDuration;
	private bool isChasing;
	private bool isSearching;
	private float searchElapsed;
	void Update()
	{
		if (isSearching)
		{
			searchElapsed += Time.deltaTime;

			if (searchElapsed > searchDuration)
			{
				BackToPatrol();
			}
		}
	}
	void LateUpdate()
	{
		if (isSearching || isChasing)
		{
			FaceVelocity();
		}
	}

	private void FaceVelocity()
	{
		directions = aIPath.desiredVelocity;
		transform.right = directions;
	}

	public void PlayerDetectEnter()
	{
		Debug.Log($"player detected, enemy {this.name} chasing");
		isSearching = false;
		isChasing = true;
	}

	public void PlayerDetectExit()
	{
		Debug.Log($"player lost, enemy {this.name} searching...");
		isSearching = true;
		searchElapsed = 0f;
	}

	private void BackToPatrol()
	{
		Debug.Log($"enemy {this.name} back to patrol");
		isSearching = false;
		isChasing = false;
	}
}
