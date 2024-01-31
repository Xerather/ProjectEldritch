using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	[SerializeField] private FloatEventChannelSO onHpUpdateChannel;
	[SerializeField] private FloatEventChannelSO onSanityUpdateChannel;
	[SerializeField] private GameObject parentPanel;
	[SerializeField] private GameObject gameOverPanel;
	[SerializeField] private GameObject levelFinishPanel;

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
		GameOver();
	}

	private void GameOverSanity(float sanity, float maxSanity)
	{
		if (sanity > 0) return;
		GameOver();
	}

	private void GameOver()
	{
		Time.timeScale = 0;
		parentPanel.SetActive(true);

		gameOverPanel.SetActive(true);
		levelFinishPanel.SetActive(false);
	}

	public void LevelFinished()
	{
		Time.timeScale = 0;
		parentPanel.SetActive(true);

		gameOverPanel.SetActive(false);
		levelFinishPanel.SetActive(true);
	}

	public void ToMainMenu()
	{
		SceneManager.LoadScene(0);
	}
}
