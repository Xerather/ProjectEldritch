using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : Characters
{
	[SerializeField] private FloatEventChannelSO playerHpChannel;
	private void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.CompareTag("Enemy"))
		{
			Debug.Log("<color=red>Player hit!</color>");
			currentHp--;
			playerHpChannel.RaiseEvent(currentHp);
		}
	}
}
