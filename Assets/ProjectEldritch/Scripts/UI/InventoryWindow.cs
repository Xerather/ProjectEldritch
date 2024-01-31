using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryWindow : MonoBehaviour
{
	[SerializeField] private InventorySO defaultInventorySO;
	[SerializeField] private List<ItemSlot> activeInventory = new List<ItemSlot>();
	[SerializeField] private ItemBox itemBoxPrefab;
	[SerializeField] private Transform itemBoxLayoutGroup;
	[SerializeField] private PowerUpEventChannelSO onPowerUpUseChannel;
	[SerializeField] private ItemEventChannelSO OnItemUseChannel;
	[SerializeField] private ItemEventChannelSO OnItemGetChannel;

	private List<ItemBox> itemBoxList = new List<ItemBox>();


	// Start is called before the first frame update
	void Start()
	{
		SetDefaultInventory();
		DestroyAllItemBox();
		CreateItemBox();
	}

	void OnEnable()
	{
		OnItemUseChannel.RegisterListener(UpdateItemBox);
		OnItemGetChannel.RegisterListener(AddItem);
	}
	void OnDisable()
	{
		OnItemUseChannel.RemoveListener(UpdateItemBox);
		OnItemGetChannel.RemoveListener(AddItem);

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
		foreach (ItemSlot slot in activeInventory)
		{
			CreateItemBox(slot);
		}
	}

	private void CreateItemBox(ItemSlot slot)
	{
		ItemBox box = Instantiate(itemBoxPrefab, itemBoxLayoutGroup);
		box.SetItem(slot, UseItemEffect);
		itemBoxList.Add(box);
	}

	private void UpdateItemBox()
	{
		List<ItemBox> removedBoxList = new List<ItemBox>();
		foreach (ItemBox box in itemBoxList)
		{
			if (box.currentItemSlot.qty < 1)
			{
				removedBoxList.Add(box);
				continue;
			}
			box.UpdateBoxDescription();
		}

		foreach (ItemBox removedBox in removedBoxList)
		{
			itemBoxList.Remove(removedBox);
			Destroy(removedBox.gameObject);
		}
	}

	private void DestroyAllItemBox()
	{
		itemBoxList.Clear();
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
			ItemSlot newSlot = new ItemSlot(itemSO, qty);
			activeInventory.Add(newSlot);
			CreateItemBox(newSlot);
		}
		else
		{
			targetSlot.qty += qty;
		}
		UpdateItemBox();
	}

	public ItemSlot GetItemSlot(ItemSO itemSO)
	{
		return activeInventory.Find((x) => x.itemSO == itemSO);
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
			}
			targetSlot.qty -= qty;
			UpdateItemBox();
			return true;
		}
		return false;
	}

	private void UpdateItemBox(ItemSO itemSO, int qty)
	{
		UpdateItemBox();
	}

	// Update is called once per frame
	private void UseItemEffect(ItemBox itemBox)
	{
		if (itemBox.itemSO is PowerUpSO)
		{
			onPowerUpUseChannel.RaiseEvent((PowerUpSO)itemBox.itemSO);
		}
		ConsumeItem(itemBox.itemSO);
	}
}
