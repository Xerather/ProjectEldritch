using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Events/Float Event Channel")]
public class FloatEventChannelSO : ScriptableObject
{
	private Action<float> OnEventRaised;

	public void RaiseEvent(float value)
	{
		OnEventRaised?.Invoke(value);
	}

	public void RegisterListener(Action<float> action)
	{
		OnEventRaised += action;
	}

	public void RemoveListener(Action<float> action)
	{
		OnEventRaised -= action;
	}
}
