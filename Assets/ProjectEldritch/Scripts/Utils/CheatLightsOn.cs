using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CheatLightsOn : MonoBehaviour
{
	[SerializeField] private Light2D globalLight;
	private bool isLightOn = false;
	private float defaultIntensity;

	void Start()
	{
		defaultIntensity = globalLight.intensity;
	}
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha0))
		{
			isLightOn = !isLightOn;
			globalLight.intensity = isLightOn ? 1 : defaultIntensity;
		}
	}
}
