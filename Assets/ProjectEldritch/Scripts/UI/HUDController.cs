using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HUDController : MonoBehaviour
{
	[SerializeField] private Image hpBar;
	[SerializeField] private FloatEventChannelSO hudChannel;

	private void OnEnable()
	{
		hudChannel.RegisterListener(UpdateBar);
	}

	private void UpdateBar(float value, float maxValue)
	{
		hpBar.fillAmount = value / maxValue;
	}
}
