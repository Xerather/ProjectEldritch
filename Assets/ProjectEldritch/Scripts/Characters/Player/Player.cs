using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Player : Characters
{
	[SerializeField] private FloatEventChannelSO playerHpChannel;
	public PlayerStatus playerStatus;
	[SerializeField] private Light2D playerLight;
	[SerializeField] private bool isSelfLight = true;
	[SerializeField] private bool isOnLight = false;
	[SerializeField] private InventorySO playerInventory;
	[SerializeField] private PlayerMovement playerMovement;
	public bool isPlayerVisible => isOnLight || isSelfLight;
	void Update()
	{
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
			playerHpChannel.RaiseEvent(playerStatus.hp);
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
}
