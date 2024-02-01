using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SanityOverlayController : MonoBehaviour
{
	[SerializeField] private FloatEventChannelSO onSanityUpdateChannel;
	[SerializeField] private Image overlay;
	[SerializeField] private float maxAlphaSanity;
	void OnEnable()
	{
		onSanityUpdateChannel.RegisterListener(UpdateOverlay);
	}
	void OnDisable()
	{
		onSanityUpdateChannel.RemoveListener(UpdateOverlay);
	}

	private void UpdateOverlay(float currentValue, float maxValue)
	{
		overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, (maxValue - currentValue) / maxValue * maxAlphaSanity / 255);
	}
}
