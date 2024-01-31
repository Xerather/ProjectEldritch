using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyMovement : MonoBehaviour
{
	[SerializeField] private AIPath aIPath;
	[SerializeField] private Vector2 directions;
	private FOVMechanic fov;
	[SerializeField] private bool isChasing;

	void Awake()
	{
		fov = GetComponent<FOVMechanic>();
	}

	void LateUpdate()
	{
		if (isChasing)
		{
			FaceVelocity();
		}
	}

	private void FaceVelocity()
	{
		directions = aIPath.desiredVelocity;
		transform.right = directions;
	}

	public void PlayerSpottedEnter()
	{
		// Debug.Log($"player detected, enemy {this.name} chasing");
		isChasing = true;
	}

	public void PlayerSpottedExit()
	{
		// Debug.Log($"player lost, enemy {this.name} searching for player...");
	}

	public void DoPatrol()
	{
		// Debug.Log($"enemy {this.name} back to patrol");
		isChasing = false;
	}

	public void StopMovement()
	{
		// Debug.Log($"enemy {this.name} stopped moving");
		isChasing = false;
	}
}
