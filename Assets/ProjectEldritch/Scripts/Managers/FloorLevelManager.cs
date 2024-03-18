using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FloorLevelManager : MonoBehaviour
{
	[SerializeField] private List<FloorLevel> floorLevelList = new();
	[SerializeField] private int startingLevel;
	[SerializeField] private TextMeshProUGUI timerText;
	[SerializeField] private FloatEventChannelSO onHpUpdateChannel;
	private float timerCounter = 0;
	private bool isGameover;

	private void OnEnable()
	{
		onHpUpdateChannel.RegisterListener(CheckGameOver);
	}

	private void OnDisable()
	{
		onHpUpdateChannel.RemoveListener(CheckGameOver);
	}

	// Start is called before the first frame update
	void Start()
	{
		foreach (FloorLevel floorLevel in floorLevelList)
		{
			floorLevel.ChangeTileMapOpacity(startingLevel >= floorLevel.floorNumber);
		}
		isGameover = false;
	}

	public bool MoveFloorLevel(int currentLevel, int targetLevel)
	{
		FloorLevel currentFloorLevel = floorLevelList.Find((x) => x.floorNumber == currentLevel);
		FloorLevel targetFloorLevel = floorLevelList.Find((x) => x.floorNumber == targetLevel);

		if (currentFloorLevel == null || targetFloorLevel == null) return false;

		bool targetIsHigher = targetLevel > currentLevel;

		currentFloorLevel.ChangeFloorState(false, targetIsHigher);
		targetFloorLevel.ChangeFloorState(true, targetIsHigher);

		return true;
	}

	public bool MoveFloorLevel(int currentLevel, Teleporter targetTeleporter)
	{
		if (targetTeleporter == null) return false;
		return MoveFloorLevel(currentLevel, targetTeleporter.targetFloorNumber);
	}

	public List<FloorLevel> GetFloorLevelList()
	{
		return floorLevelList;
	}

	public FloorLevel GetFloorLevel(int floorLevel)
	{
		return floorLevelList.Find((x) => x.floorNumber == floorLevel);
	}

	void Update()
	{
		if (isGameover) return;
		timerCounter += Time.deltaTime;
		int seconds = (int)timerCounter % 60;
		int minutes = (int)timerCounter / 60;
		string.Format("{0:00}:{1:00}", minutes, seconds);

		timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
	}

	private void CheckGameOver(float hp, float maxHp)
	{
		if (hp > 0) return;
		isGameover = true;
	}
}
