using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Events/PowerUp Event Channel")]
public class PowerUpEventChannelSO : ScriptableObject
{
	private Action<PowerUpSO> OnEventRaised;

	public void RaiseEvent(PowerUpSO powerUpSO)
	{
		OnEventRaised?.Invoke(powerUpSO);
	}

	public void RegisterListener(Action<PowerUpSO> action)
	{
		OnEventRaised += action;
	}

	public void RemoveListener(Action<PowerUpSO> action)
	{
		OnEventRaised -= action;
	}
}
