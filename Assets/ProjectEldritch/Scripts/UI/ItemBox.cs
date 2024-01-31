using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemBox : MonoBehaviour
{
	public ItemSlot currentItemSlot;
	public ItemSO itemSO => currentItemSlot.itemSO;
	[SerializeField] private TextMeshProUGUI nameText;
	[SerializeField] private TextMeshProUGUI qtyText;
	[SerializeField] private Image itemImg;
	[SerializeField] private Button button;
	private Action<ItemBox> OnUseAction;

	// Start is called before the first frame update
	void Start()
	{
		button.onClick.AddListener(UseItem);
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void SetItem(ItemSlot itemSlot, Action<ItemBox> OnUseAction)
	{
		currentItemSlot = itemSlot;
		UpdateBoxDescription();
		this.OnUseAction = OnUseAction;
		button.interactable = itemSO is PowerUpSO;
	}

	public void UpdateBoxDescription()
	{
		nameText.text = currentItemSlot.itemSO.itemName;
		qtyText.text = currentItemSlot.qty + "x";
		itemImg.sprite = currentItemSlot.itemSO.inventorySprite;
	}

	public void UseItem()
	{
		Debug.Log("use item " + itemSO.name);
		OnUseAction?.Invoke(this);
	}
}
