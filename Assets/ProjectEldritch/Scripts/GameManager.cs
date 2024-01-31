using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	[SerializeField] private FloatEventChannelSO onHpUpdateChannel;
	[SerializeField] private FloatEventChannelSO onSanityUpdateChannel;
	[SerializeField] private GameObject gameOverPanel;

	private void Awake()
	{
		instance = this;
	}
	private void OnEnable()
	{
		onHpUpdateChannel.RegisterListener(GameOverHp);
		onSanityUpdateChannel.RegisterListener(GameOverSanity);
	}

	private void OnDisable()
	{
		onHpUpdateChannel.RemoveListener(GameOverHp);
		onSanityUpdateChannel.RemoveListener(GameOverSanity);
	}

	private void GameOverHp(float hp, float maxHp)
	{
		if (hp > 0) return;
		else
		{
			gameOverPanel.SetActive(true);
		}
	}

	private void GameOverSanity(float sanity, float maxSanity)
	{
		if (sanity > 0) return;
		else
		{
			gameOverPanel.SetActive(true);
		}
	}
}
