using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Data/Inventory SO")]
public class InventorySO : ScriptableObject
{
	public List<ItemSlot> itemSlotList = new List<ItemSlot>();
}

[System.Serializable]
public class ItemSlot
{
	public ItemSlot() { }
	public ItemSlot(ItemSlot slot)
	{
		itemSO = slot.itemSO;
		qty = slot.qty;
	}

	public ItemSlot(ItemSO itemSO, int qty)
	{
		this.itemSO = itemSO;
		this.qty = qty;
	}
	public ItemSO itemSO;
	public int qty;
}
