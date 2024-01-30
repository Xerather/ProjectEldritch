using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyMovement : MonoBehaviour
{
	[SerializeField] private AIPath aIPath;

	[SerializeField] private Vector2 directions;
	void Start()
	{

	}

	// Update is called once per frame
	void LateUpdate()
	{
		FaceVelocity();
	}

	private void FaceVelocity()
	{
		directions = aIPath.desiredVelocity;
		transform.right = directions;
	}

	public void PlayerDetectEnter()
	{
		Debug.Log("player detected, chasing");
	}

	public void PlayerDetectExit()
	{
		Debug.Log("player lost, patrolling");
	}
}
