using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleportHandle : MonoBehaviour
{
	[SerializeField] private Player player;
	public Teleporter teleporter;
	public bool canTeleport => teleporter != null && teleporter.isTeleportable;
	// Start is called before the first frame update

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.CompareTag("Teleporter"))
		{
			Teleporter newteleporter = col.GetComponent<Teleporter>();
			if (newteleporter.floorNumber != player.floorNumber) return;
			teleporter = newteleporter;
		}
	}

	private void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.CompareTag("Teleporter"))
		{
			teleporter = null;
		}
	}
}
