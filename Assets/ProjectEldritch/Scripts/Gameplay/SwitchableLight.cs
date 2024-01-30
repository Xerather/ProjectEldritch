using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SwitchableLight : MonoBehaviour
{
	[SerializeField] private Light2D light2d;
	[SerializeField] private GameObject fireObj;
	[SerializeField] private bool litOnStart = true;
	private bool inRange = false;

	void Start()
	{

		SwitchLight();
	}

	void Update()
	{
		if (inRange && Input.GetKeyDown(KeyCode.Q))
		{
			SwitchLight();
		}
	}

	private void SwitchLight()
	{
		light2d.enabled = !light2d.enabled;
		fireObj?.SetActive(light2d.enabled);
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (!collider.gameObject.CompareTag("Player")) return;
		inRange = true;
	}

	private void OnTriggerExit2D(Collider2D collider)
	{

		if (!collider.gameObject.CompareTag("Player")) return;
		inRange = false;
	}
}
