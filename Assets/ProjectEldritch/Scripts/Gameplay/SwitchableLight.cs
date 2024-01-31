using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SwitchableLight : InteractableObject
{
	[SerializeField] private Light2D light2d;
	[SerializeField] private GameObject fireObj;
	[SerializeField] private bool litOnStart = true;

	protected override void Start()
	{
		base.Start();
		light2d.enabled = litOnStart;
		fireObj?.SetActive(litOnStart);
	}

	protected override void TriggerInteraction()
	{
		base.TriggerInteraction();
		SwitchLight();
	}

	private void SwitchLight()
	{
		light2d.enabled = !light2d.enabled;
		fireObj?.SetActive(light2d.enabled);
	}
}
