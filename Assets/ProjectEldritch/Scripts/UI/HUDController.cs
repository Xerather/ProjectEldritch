using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HUDController : MonoBehaviour
{
	[SerializeField] private Image hpBar;
	[SerializeField] private FloatEventChannelSO playerHpChannel;

	private void OnEnable()
	{
		playerHpChannel.RegisterListener(UpdateHpBar);
	}

	private void UpdateHpBar(float hp)
	{
		hpBar.fillAmount = hp / 3;
	}
}
