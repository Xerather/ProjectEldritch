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
	[SerializeField] private PlayerMovement playerMovement;
	[SerializeField] private InventoryWindow inventorySO;

	[Header("Slash")]
	[SerializeField] private GameObject vfx_slash;
	[SerializeField] private Transform slashPosition;

	[Header("Shuriken")]
	[SerializeField] private GameObject shurikenPrefab;

	[Header("Teleport")]
	[SerializeField] private Transform jumpPosition;

	private JumpAssasination jumpAssasinationHandler;
	private float hitInvicibiltyCounter;
	[SerializeField] private PlayerTeleportHandle playerTeleportHandle;
	void Awake()
	{
		playerStats = new PlayerStats(playerSO.playerStats);
		jumpAssasinationHandler = GetComponentInChildren<JumpAssasination>();
		jumpAssasinationHandler.Setup(this);
	}

	protected override void Start()
	{
		base.Start();
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

		if (hitInvicibiltyCounter > 0) hitInvicibiltyCounter -= Time.deltaTime;
	}

	private void HandleAttack()
	{
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			SpawnSlash();
		}
		if (Input.GetKeyDown(KeyCode.Mouse1))
		{
			// SpawnShuriken();
		}
	}

	private void HandleInteraction()
	{
		if (Input.GetKeyDown(KeyCode.F))
		{
			if (playerTeleportHandle.canTeleport) DoTeleport();
			if (jumpAssasinationHandler.canAssasinate && floorNumber > 0)
			{
				DoJumpAssasination();
			}
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (floorNumber > 0)
			{
				DoJump();
			}
		}
	}

	private void DoJumpAssasination()
	{
		Enemy enemy = jumpAssasinationHandler.GetClosestEnemy();
		enemy.GetAssasinated();
		DoJump();
	}

	private void DoTeleport()
	{
		Teleporter teleporter = playerTeleportHandle.teleporter;
		if (!teleporter.isTeleportable) return;
		if (!floorLevelManager.MoveFloorLevel(floorNumber, teleporter)) return;

		SetLayerCollision(floorLevelManager.GetFloorLevel(floorNumber), true);
		SetLayerCollision(teleporter.targetFloorLevel, false);

		transform.position = teleporter.targetSpawner.position;
		floorNumber = teleporter.targetFloorNumber;
	}

	private void DoJump()
	{
		if (!floorLevelManager.MoveFloorLevel(floorNumber, floorNumber - 1)) return;

		SetLayerCollision(floorLevelManager.GetFloorLevel(floorNumber), true);
		SetLayerCollision(floorLevelManager.GetFloorLevel(floorNumber - 1), false);

		transform.position = jumpPosition.position;
		floorNumber--;
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

	public void GetHit(float hitDamage)
	{
		if (hitInvicibiltyCounter > 0) return;

		if (playerStats.hp <= 0) return;

		playerStats.hp -= hitDamage;
		onHpUpdateChannel.RaiseEvent(playerStats.hp, playerStats.maxHp);
		hitInvicibiltyCounter = playerStats.hitInvicibiltyCooldown;

		StartCoroutine(BlinkingRed());
	}
}
