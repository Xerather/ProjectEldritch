using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Player : Characters
{
	[SerializeField] private FloatEventChannelSO onHpUpdateChannel;
	[SerializeField] private FloatEventChannelSO onSanityUpdateChannel;
	[SerializeField] private PowerUpEventChannelSO onPowerUpUseChannel;

	public PlayerStatus playerStatus;
	[SerializeField] private Light2D playerLight;
	[SerializeField] private bool isSelfLight = true;
	[SerializeField] private bool isOnLight = false;
	[SerializeField] private PlayerMovement playerMovement;
	[SerializeField] private InventoryWindow inventorySO;
	public bool isPlayerVisible => isOnLight || isSelfLight;
	void OnEnable()
	{
		onPowerUpUseChannel.RegisterListener(UsePowerUp);
		// onItemAddChannel.RegisterListener(AddItem);
		// onItemUseChannel.RegisterListener(ConsumeItem);
	}

	void OnDisable()
	{
		onPowerUpUseChannel.RemoveListener(UsePowerUp);
	}
	void Update()
	{
		if (!isPlayerVisible)
		{
			if (playerStatus.sanity < 0) return;
			playerStatus.sanity -= Time.deltaTime;
			onSanityUpdateChannel.RaiseEvent(playerStatus.sanity, playerStatus.maxSanity);
		}

		if (Input.GetKeyDown(KeyCode.F))
		{
			isSelfLight = !isSelfLight;

			playerLight.pointLightInnerRadius = isSelfLight ? playerStatus.onInnerRadius : playerStatus.offInnerRadius;
			playerLight.pointLightOuterRadius = isSelfLight ? playerStatus.onOuterRadius : playerStatus.offOuterRadius;
		}
	}
	private void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.CompareTag("Enemy"))
		{
			Debug.Log("<color=red>Player hit!</color>");
			playerStatus.hp--;
			playerStatus.sanity -= 10;
			onHpUpdateChannel.RaiseEvent(playerStatus.hp, playerStatus.maxHp);
			onSanityUpdateChannel.RaiseEvent(playerStatus.sanity, playerStatus.maxSanity);

			playerMovement.rb.freezeRotation = true;
			playerMovement.rb.freezeRotation = false;
		}
	}

	private void OnTriggerStay2D(Collider2D col)
	{
		if (col.gameObject.CompareTag("Light"))
		{
			isOnLight = true;
		}
	}

	private void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.CompareTag("Light"))
		{
			isOnLight = false;
		}
	}

	public void AddItem(ItemSO itemSO, int qty = 1)
	{
		inventorySO.AddItem(itemSO, qty);
	}
	public bool ConsumeItem(ItemSO itemSO, int qty = 1)
	{
		return inventorySO.ConsumeItem(itemSO, qty);
	}

	private void UsePowerUp(PowerUpSO powerUp)
	{
		playerStatus.AddStats(powerUp.additionalStatus);
		onHpUpdateChannel.RaiseEvent(playerStatus.hp, playerStatus.maxHp);
		onSanityUpdateChannel.RaiseEvent(playerStatus.sanity, playerStatus.maxSanity);
	}
}
