﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PatrolMechanic : MonoBehaviour
{

	public Transform patrolPointsHolder;
	public float movementSpeed = 2.0f;
	public float rotationSpeed = 10.0f;
	public bool rotate = true;
	[Tooltip("minimal 3 point (1 > 2 > 3 > 2 > 1 repeat) ")]
	public bool backtrack = true;
	[Tooltip("minimal 2 point (1 > 2 > 3 > 1 > 2 > 3 repeat) ")]
	public bool loop;

	private Rigidbody2D rb;
	private List<Transform> patrolPoints;
	private Transform nextPatrolPoint;
	private int index;

	//Getting a refrence to the RigidBody2D and getting all the transforms on the patrolPointsHolder object
	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		patrolPoints = new List<Transform>();
		foreach (Transform child in patrolPointsHolder)
		{
			patrolPoints.Add(child);
		}
	}

	//Calling the PatrolLogic methode every frame
	private void FixedUpdate()
	{
		PatrolLogic();
	}

	//All of the logic for the patrol mechanics and rotation
	private void PatrolLogic()
	{
		nextPatrolPoint = patrolPoints[index];
		if (Vector2.Distance(transform.position, nextPatrolPoint.position) < 0.1f)
		{
			index++;

			if (index == patrolPoints.Count && loop || index == patrolPoints.Count && backtrack)
				index = 0;

			if (backtrack)
				patrolPoints.Reverse();
		}
		rb.MovePosition(Vector2.MoveTowards(rb.position, nextPatrolPoint.position, movementSpeed * Time.fixedDeltaTime));

		if (rotate)
		{
			Vector2 directionToPatrolPoint = nextPatrolPoint.position - transform.position;
			float angleToPatrolPoint = Mathf.Atan2(directionToPatrolPoint.y, directionToPatrolPoint.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angleToPatrolPoint, Vector3.forward), rotationSpeed * Time.fixedDeltaTime);
		}
	}

}
