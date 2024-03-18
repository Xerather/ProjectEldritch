using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakSideCollider : MonoBehaviour
{
	public bool canBeHit;

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.CompareTag("Player"))
		{
			canBeHit = true;
		}
	}

	private void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.CompareTag("Player"))
		{
			canBeHit = false;
		}
	}
}
