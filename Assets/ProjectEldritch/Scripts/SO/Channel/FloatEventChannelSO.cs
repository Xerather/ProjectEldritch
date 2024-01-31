using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Events/Float Event Channel")]
public class FloatEventChannelSO : ScriptableObject
{
	private Action<float, float> OnEventRaised;

	public void RaiseEvent(float value, float maxValue)
	{
		OnEventRaised?.Invoke(value, maxValue);
	}

	public void RegisterListener(Action<float, float> action)
	{
		OnEventRaised += action;
	}

	public void RemoveListener(Action<float, float> action)
	{
		OnEventRaised -= action;
	}
}
