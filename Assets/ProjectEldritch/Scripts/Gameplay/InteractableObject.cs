using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
	[SerializeField] protected TooltipEventChannelSO tooltipMoverChannel;
	protected bool inRange = false;

	protected virtual void Start() { }

	protected virtual void Update()
	{
		if (inRange && Input.GetKeyDown(KeyCode.Q))
		{
			TriggerInteraction();
		}
	}

	protected virtual void TriggerInteraction()
	{
	}

	protected virtual void OnTriggerEnter2D(Collider2D collider)
	{
		if (!collider.gameObject.CompareTag("Player")) return;
		tooltipMoverChannel.RaiseEvent(true, this.transform);
		inRange = true;
	}

	protected virtual void OnTriggerExit2D(Collider2D collider)
	{
		if (!collider.gameObject.CompareTag("Player")) return;

		tooltipMoverChannel.RaiseEvent(false, this.transform);
		inRange = false;
	}
}
