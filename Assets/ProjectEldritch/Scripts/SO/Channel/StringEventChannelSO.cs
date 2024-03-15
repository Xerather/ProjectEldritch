using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Events/String Event Channel")]
public class StringEventChannelSO : ScriptableObject
{
	private Action<string> OnEventRaised;

	public void RaiseEvent(string value)
	{
		OnEventRaised?.Invoke(value);
	}

	public void RegisterListener(Action<string> action)
	{
		OnEventRaised += action;
	}

	public void RemoveListener(Action<string> action)
	{
		OnEventRaised -= action;
	}
}
