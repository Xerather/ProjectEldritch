using System.Collections;
using UnityEngine.Tilemaps;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
	[SerializeField] private Teleporter targetTeleporter;
	[SerializeField] private TooltipEventChannelSO interactionMessageChannel;
	[SerializeField] private string interactionMessage;
	[SerializeField] private Transform playerSpawner;
	public FloorLevel floorLevel;
	public Transform targetSpawner => targetTeleporter.GetSpawner();
	public int targetFloorLevel => targetTeleporter.GetFloorLevel;
	public int GetFloorLevel => floorLevel.level;

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.CompareTag("Player"))
		{
			// interactionMessageChannel?.RaiseEvent(true, transform);
		}
	}

	public Transform GetSpawner()
	{
		return playerSpawner;
	}
}
