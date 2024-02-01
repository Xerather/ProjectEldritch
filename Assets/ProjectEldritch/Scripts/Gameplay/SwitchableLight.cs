using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SwitchableLight : InteractableObject
{
	[SerializeField] private Light2D light2d;
	[SerializeField] private Collider2D detection;
	[SerializeField] private GameObject fireObj;
	[SerializeField] private bool litOnStart = true;
	protected override void Start()
	{
		base.Start();
		SwitchLight(litOnStart);
	}

	protected override void TriggerInteraction()
	{
		base.TriggerInteraction();
		SwitchLight(!light2d.enabled);
	}

	private void SwitchLight(bool isLit)
	{
		light2d.enabled = isLit;
		detection.enabled = isLit;
		fireObj?.SetActive(isLit);
		if (isLit) sfx.Play();
	}
}
