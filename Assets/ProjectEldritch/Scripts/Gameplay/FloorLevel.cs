using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FloorLevel : MonoBehaviour
{
	public int floorNumber;
	[SerializeField] private List<Tilemap> tilemapList = new();
	[SerializeField] private List<TilemapCollider2D> collider2DList = new();
	void Awake()
	{
		tilemapList.AddRange(GetComponentsInChildren<Tilemap>());
		collider2DList.AddRange(GetComponentsInChildren<TilemapCollider2D>());
	}

	public void ChangeFloorState(bool isTarget, bool targetIsHigher)
	{
		ChangeTileMapOpacity(isTarget || targetIsHigher);
	}

	public void ChangeTileMapOpacity(bool targetIsActiveOrHigher)
	{
		float currentAlpha = targetIsActiveOrHigher ? 1f : .4f;

		foreach (Tilemap tilemap in tilemapList)
		{
			tilemap.color = new Color(255, 255, 255, currentAlpha);
		}
	}

	public int GetLayerId()
	{
		return gameObject.layer;
	}
}
