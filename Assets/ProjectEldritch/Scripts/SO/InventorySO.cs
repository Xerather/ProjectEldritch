using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Data/Inventory SO")]
public class InventorySO : ScriptableObject
{
	public List<ItemSlot> ownedItemList = new List<ItemSlot>();

	public void AddItem(ItemSO itemSO, int qty = 1)
	{
		ItemSlot targetSlot = GetItemSlot(itemSO);
		if (targetSlot == null)
		{
			ownedItemList.Add(new ItemSlot(itemSO, qty));
		}
		else
		{
			targetSlot.qty += qty;
		}
	}

	public ItemSlot GetItemSlot(ItemSO itemSO)
	{
		return ownedItemList.Find((x) => x.itemSO = itemSO);
	}

	public bool ConsumeItem(ItemSO itemSO, int qty = 1)
	{
		ItemSlot targetSlot = GetItemSlot(itemSO);
		if (targetSlot == null) return false;

		if (targetSlot.qty - qty >= 0)
		{
			if (targetSlot.qty - qty == 0)
			{
				ownedItemList.Remove(targetSlot);
				return true;
			}
			targetSlot.qty -= qty;
			return true;
		}
		return false;
	}
}

[System.Serializable]
public class ItemSlot
{
	public ItemSlot() { }
	public ItemSlot(ItemSO itemSO, int qty)
	{
		this.itemSO = itemSO;
		this.qty = qty;
	}
	public ItemSO itemSO;
	public int qty;
}
