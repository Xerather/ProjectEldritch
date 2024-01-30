using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters : MonoBehaviour
{
	[SerializeField] protected CharactersSO characterSO;
	[SerializeField] protected PlayerMovement playerMovement;
	protected float currentHp;
	protected Vector3 spawnPos;
	protected virtual void Start()
	{
		spawnPos = transform.position;
		Reset();
	}

	private void Reset()
	{
		transform.position = spawnPos;
		currentHp = characterSO.maxHp;
		playerMovement.moveSpeed = characterSO.moveSpeed;
	}
}
