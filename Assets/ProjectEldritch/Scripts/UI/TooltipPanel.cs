using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipPanel : MonoBehaviour
{
	[SerializeField] private TooltipEventChannelSO tooltipActivatorChannel;
	[SerializeField] private GameObject tooltip;
	[SerializeField] private float offsetY;
	void OnEnable()
	{
		tooltipActivatorChannel.RegisterListener(ActivateTooltip);
	}

	// Update is called once per frame
	void ActivateTooltip(bool IsActive, Transform target)
	{
		tooltip.SetActive(IsActive);
		if (IsActive == false) return;
		Vector3 offset = new Vector3(0, offsetY, 0);
		transform.position = Camera.main.WorldToScreenPoint(target.position) + offset;
	}
}
