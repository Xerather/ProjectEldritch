using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class FloorLevelManager : MonoBehaviour
{
	[SerializeField] private List<FloorLevel> floorLevelList = new();
	[SerializeField] private int startingLevel;
	// Start is called before the first frame update
	void Start()
	{
		foreach (FloorLevel floorLevel in floorLevelList)
		{
			floorLevel.ChangeTileMapOpacity(startingLevel >= floorLevel.floorNumber);
			floorLevel.ActivateCollider(floorLevel.floorNumber == startingLevel);
		}
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
}
