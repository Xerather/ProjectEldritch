using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : InteractableObject
{
	[SerializeField] private ItemSlot itemSlot;
	[SerializeField] private ItemEventChannelSO onItemGetChannel;
	[SerializeField] private GameObject dummyAudioSource;

	protected override void TriggerInteraction()
	{
		base.TriggerInteraction();
		GetItem();
	}

	private void GetItem()
	{
		Instantiate(dummyAudioSource);

		onItemGetChannel.RaiseEvent(itemSlot.itemSO, itemSlot.qty);
		Destroy(this.gameObject);
	}
}
