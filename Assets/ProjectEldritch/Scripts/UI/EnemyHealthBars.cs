using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBars : MonoBehaviour
{
	[SerializeField] private Enemy enemy;
	[SerializeField] private GameObject health;
	[SerializeField] private Image healthFill;
	[SerializeField] private bool alwaysShowHealth;
	[Header("config")]
	[SerializeField] private bool showHealth;
	[SerializeField] private float showDistance;
	[SerializeField] private float showDuration;
	[SerializeField] private float showtimerCounter;
	[SerializeField] private Transform targetPosition;
	[SerializeField] private Vector3 offset;
	[SerializeField] private FloatEventChannelSO onEnemyGotHit;
	private Player player;


	void Start()
	{
		player = FindObjectOfType<Player>();
		showHealth = false;
	}

	void OnEnable()
	{
		onEnemyGotHit.RegisterListener(ShowHealthBar);
	}

	void OnDisable()
	{
		onEnemyGotHit.RemoveListener(ShowHealthBar);
	}

	// Update is called once per frame
	void Update()
	{
		LockPosition();
		CheckHealthValue();
		CheckHealthVisibility();
	}

	private void LockPosition()
	{
		transform.rotation = Camera.main.transform.rotation;
		transform.position = targetPosition.position + offset;
	}

	private void CheckHealthValue()
	{
		healthFill.fillAmount = enemy.hp / enemy.maxHp;
	}

	private void CheckHealthVisibility()
	{
		if (showtimerCounter > 0)
		{
			showtimerCounter -= Time.deltaTime;
			if (showtimerCounter <= 0)
			{
				showHealth = false;
			}
		}

		if (Vector3.Distance(player.transform.position, transform.position) > showDistance)
		{
			showHealth = false;
		}

		if (alwaysShowHealth) showHealth = true;
		health.SetActive(showHealth);
	}

	private void ShowHealthBar(float value1, float value2)
	{
		showHealth = true;
		showtimerCounter = showDuration;
	}
}
