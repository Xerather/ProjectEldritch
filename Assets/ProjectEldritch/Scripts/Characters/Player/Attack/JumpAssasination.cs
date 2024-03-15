using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAssasination : MonoBehaviour
{
	[SerializeField] TooltipEventChannelSO interactionMessageChannel;
	[SerializeField] private Enemy enemy;
	public bool canAssasinate => enemy != null;
	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag("Enemy"))
		{
			// interactionMessageChannel.RaiseEvent()
			enemy = col.GetComponent<Enemy>();
		}
	}

	private void OnTriggerExit2D(Collider2D col)
	{
		if (col.CompareTag("Enemy"))
		{
			// interactionMessageChannel.RaiseEvent()
			enemy = null;
		}
	}
}
