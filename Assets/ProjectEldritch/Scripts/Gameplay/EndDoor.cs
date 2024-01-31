using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDoor : InteractableObject
{
	[SerializeField] private ItemSO itemSO;
	[SerializeField] private Player player;

	protected override void TriggerInteraction()
	{
		sfx.Play();
		bool canUse = player.ConsumeItem(itemSO);
		Debug.Log("have key " + itemSO.name + " in inventory ? " + canUse);
		if (!canUse) return;

		base.TriggerInteraction();
		GameManager.instance.LevelFinished();
	}
}
