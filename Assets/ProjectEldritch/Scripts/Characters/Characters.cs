using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters : MonoBehaviour
{
	protected Vector3 spawnPos;
	protected virtual void Start()
	{
		spawnPos = transform.position;
		Reset();
	}

	private void Reset()
	{
		transform.position = spawnPos;
		// currentHp = characterSO.maxHp;
		// playerMovement.moveSpeed = characterSO.moveSpeed;
	}
}
