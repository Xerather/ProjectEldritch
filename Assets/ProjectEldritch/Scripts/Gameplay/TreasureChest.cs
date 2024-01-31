using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : MonoBehaviour
{
	[SerializeField] private ItemSlot itemSlot;
	[SerializeField] private TooltipEventChannelSO tooltipMoverChannel;
	[SerializeField] private ItemEventChannelSO onItemGetChannel;
	private bool inRange = false;

	void Update()
	{
		if (inRange && Input.GetKeyDown(KeyCode.Q))
		{
			GetItem();
		}
	}

	private void GetItem()
	{
		onItemGetChannel.RaiseEvent(itemSlot.itemSO, itemSlot.qty);
		Destroy(this.gameObject);
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (!collider.gameObject.CompareTag("Player")) return;
		tooltipMoverChannel.RaiseEvent(true, this.transform);
		inRange = true;
	}

	private void OnTriggerExit2D(Collider2D collider)
	{
		if (!collider.gameObject.CompareTag("Player")) return;

		tooltipMoverChannel.RaiseEvent(false, this.transform);
		inRange = false;
	}
}
