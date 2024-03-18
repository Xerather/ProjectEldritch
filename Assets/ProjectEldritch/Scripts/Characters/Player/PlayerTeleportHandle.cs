using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleportHandle : MonoBehaviour
{
	public Teleporter teleporter;
	public bool canTeleport => teleporter != null;
	// Start is called before the first frame update

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.CompareTag("Teleporter"))
		{
			teleporter = col.GetComponent<Teleporter>();
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
