using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : InteractableObject
{
	[SerializeField] private ItemSlot itemSlot;
	[SerializeField] private ItemEventChannelSO onItemGetChannel;

	protected override void TriggerInteraction()
	{
		base.TriggerInteraction();
		GetItem();
	}

	private void GetItem()
	{
		onItemGetChannel.RaiseEvent(itemSlot.itemSO, itemSlot.qty);
		Destroy(this.gameObject);
	}
}
