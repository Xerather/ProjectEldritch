using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemBox : MonoBehaviour
{
	public ItemSlot currentItemSlot;

	[SerializeField] private TextMeshProUGUI nameText;
	[SerializeField] private TextMeshProUGUI qtyText;
	[SerializeField] private Image itemImg;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void SetItem(ItemSlot itemSlot)
	{
		currentItemSlot = itemSlot;
		UpdateBoxDescription();
	}

	public void UpdateBoxDescription()
	{
		nameText.text = currentItemSlot.itemSO.itemName;
		qtyText.text = currentItemSlot.qty + "x";
		itemImg.sprite = currentItemSlot.itemSO.inventorySprite;
	}
}
