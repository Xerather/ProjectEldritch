using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	[SerializeField] private FloatEventChannelSO playerHpChannel;
	[SerializeField] private GameObject gameOverPanel;
	[SerializeField] private InventorySO inventorySO;

	[Header("GameStats")]
	public float torch;
	public float hp;

	private void Awake()
	{
		instance = this;
	}
	private void OnEnable()
	{
		playerHpChannel.RegisterListener(GameOver);
	}

	private void GameOver(float hp)
	{
		if (hp > 0) return;
		else
		{
			gameOverPanel.SetActive(true);
		}
	}
}
