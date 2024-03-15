using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Player : Characters
{
	[SerializeField] private PlayerSO playerSO;
	[SerializeField] private FloatEventChannelSO onHpUpdateChannel;
	[SerializeField] private PowerUpEventChannelSO onPowerUpUseChannel;
	[SerializeField] private SoundMaker soundMaker;
	public PlayerStats playerStats;
	[SerializeField] private bool isSelfLight = true;
	[SerializeField] private bool isOnLight = false;
	[SerializeField] private PlayerMovement playerMovement;
	[SerializeField] private InventoryWindow inventorySO;
	public bool isPlayerVisible => isOnLight || isSelfLight;

	[Header("Slash")]
	[SerializeField] private GameObject vfx_slash;
	[SerializeField] private Transform slashPosition;

	[Header("Shuriken")]
	[SerializeField] private GameObject shurikenPrefab;

	[Header("Teleport")]
	[SerializeField] private Teleporter teleporter = null;
	private bool canTeleport => teleporter != null;
	void Awake()
	{
		playerStats = new PlayerStats(playerSO.playerStats);
	}

	void OnEnable()
	{
		onPowerUpUseChannel.RegisterListener(UsePowerUp);
	}

	void OnDisable()
	{
		onPowerUpUseChannel.RemoveListener(UsePowerUp);
	}
	void Update()
	{
		HandleInteraction();
		HandleAttack();
	}

	private void HandleAttack()
	{
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			SpawnSlash();
		}
		if (Input.GetKeyDown(KeyCode.Mouse1))
		{
			SpawnShuriken();
		}
	}

	private void HandleInteraction()
	{
		if (Input.GetKeyDown(KeyCode.F))
		{
			if (canTeleport) DoTeleport();
		}
	}

	private void DoTeleport()
	{
		transform.position = teleporter.targetSpawner.position;
		teleporter.ChangeMapOpacity();
	}

	private void SpawnSlash()
	{
		Instantiate(vfx_slash, slashPosition);
	}

	private void SpawnShuriken()
	{
		Instantiate(shurikenPrefab, slashPosition.position, playerMovement.faceDirection.rotation);
	}

	private void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.CompareTag("Enemy"))
		{
			Debug.Log("<color=red>Player hit!</color>");
			playerStats.hp--;
			onHpUpdateChannel.RaiseEvent(playerStats.hp, playerStats.maxHp);
		}
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		Debug.Log("enter");
		if (col.gameObject.CompareTag("Teleporter"))
		{
			teleporter = col.GetComponent<Teleporter>();
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

		if (col.gameObject.CompareTag("Teleporter"))
		{
			teleporter = null;
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
		playerStats.AddStats(powerUp.additionalStats);
		onHpUpdateChannel.RaiseEvent(playerStats.hp, playerStats.maxHp);
	}
}
