using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Events/Bool Event Channel")]
public class BoolEventChannelSO : ScriptableObject
{
	private Action<bool> OnEventRaised;

	public void RaiseEvent(bool value)
	{
		OnEventRaised?.Invoke(value);
	}

	public void RegisterListener(Action<bool> action)
	{
		OnEventRaised += action;
	}

	public void RemoveListener(Action<bool> action)
	{
		OnEventRaised -= action;
	}
}
