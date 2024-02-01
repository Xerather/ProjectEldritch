using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestContentRandomizer : MonoBehaviour
{
	[SerializeField] List<TreasureChest> treasureChestList = new List<TreasureChest>();
	[SerializeField] List<ItemSlot> itemSlotList = new List<ItemSlot>();
	// Start is called before the first frame update
	void Start()
	{
		ShuffleChestContent();
	}

	private void ShuffleChestContent()
	{
		foreach (TreasureChest chest in treasureChestList)
		{
			itemSlotList.Add(chest.itemSlot);
		}

		ShuffleList(itemSlotList);

		for (int i = 0; i < itemSlotList.Count; i++)
		{
			treasureChestList[i].itemSlot = itemSlotList[i];
		}
	}

	public List<ItemSlot> ShuffleList(List<ItemSlot> list)
	{
		for (int i = list.Count - 1; i > 0; i--)
		{
			int j = UnityEngine.Random.Range(0, i + 1);
			ItemSlot temp = list[i];
			list[i] = list[j];
			list[j] = temp;
		}
		return list;
	}
}
