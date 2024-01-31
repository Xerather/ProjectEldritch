using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryWindow : MonoBehaviour
{
	[SerializeField] private InventorySO defaultInventorySO;
	[SerializeField] private List<ItemSlot> activeInventory = new List<ItemSlot>();
	[SerializeField] private ItemBox itemBoxPrefab;
	[SerializeField] private Transform itemBoxLayoutGroup;
	// Start is called before the first frame update
	void Start()
	{
		SetDefaultInventory();
		CreateItemBox();
	}

	private void SetDefaultInventory()
	{
		foreach (ItemSlot slot in defaultInventorySO.itemSlotList)
		{
			activeInventory.Add(new ItemSlot(slot));
		}
	}

	private void CreateItemBox()
	{
		DestroyAllItemBox();

		foreach (ItemSlot slot in defaultInventorySO.itemSlotList)
		{
			ItemBox box = Instantiate(itemBoxPrefab, itemBoxLayoutGroup);
			box.SetItem(slot);
		}
	}

	private void DestroyAllItemBox()
	{
		for (int i = 0; i < itemBoxLayoutGroup.childCount; i++)
		{
			Destroy(itemBoxLayoutGroup.GetChild(i).gameObject);
		}
	}

	public void AddItem(ItemSO itemSO, int qty = 1)
	{
		ItemSlot targetSlot = GetItemSlot(itemSO);
		if (targetSlot == null)
		{
			activeInventory.Add(new ItemSlot(itemSO, qty));
		}
		else
		{
			targetSlot.qty += qty;
		}
	}

	public ItemSlot GetItemSlot(ItemSO itemSO)
	{
		return activeInventory.Find((x) => x.itemSO = itemSO);
	}

	public bool ConsumeItem(ItemSO itemSO, int qty = 1)
	{
		ItemSlot targetSlot = GetItemSlot(itemSO);
		if (targetSlot == null) return false;

		if (targetSlot.qty - qty >= 0)
		{
			if (targetSlot.qty - qty == 0)
			{
				activeInventory.Remove(targetSlot);
				return true;
			}
			targetSlot.qty -= qty;
			return true;
		}
		return false;
	}

	// Update is called once per frame
	void Update()
	{

	}
}
