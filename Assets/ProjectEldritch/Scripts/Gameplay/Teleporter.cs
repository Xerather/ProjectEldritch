using System.Collections;
using UnityEngine.Tilemaps;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
	[SerializeField] private Teleporter targetTeleporter;
	[SerializeField] private Tilemap currentMap;
	[SerializeField] private Tilemap targetMap;
	[SerializeField] private TooltipEventChannelSO interactionMessageChannel;
	[SerializeField] private string interactionMessage;
	[SerializeField] private Transform playerSpawner;
	public Transform targetSpawner => targetTeleporter.GetSpawner();
	public int floorLevel;
	private bool targetIsHigher => targetTeleporter.floorLevel > floorLevel;

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.CompareTag("Player"))
		{
			// interactionMessageChannel?.RaiseEvent(true, transform);
		}
	}

	public void ChangeMapOpacity()
	{
		float nextCurrentAlpha = targetIsHigher ? 1f : .4f;

		currentMap.color = new Color(255, 255, 255, nextCurrentAlpha);
		targetMap.color = new Color(255, 255, 255, 1f);
	}

	public Transform GetSpawner()
	{
		return playerSpawner;
	}
}
