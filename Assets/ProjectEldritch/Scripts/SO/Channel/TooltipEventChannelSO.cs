using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Events/Tooltip Event Channel")]
public class TooltipEventChannelSO : ScriptableObject
{
	private Action<bool, Transform> OnEventRaised;

	public void RaiseEvent(bool IsActive, Transform transform)
	{
		OnEventRaised?.Invoke(IsActive, transform);
	}

	public void RegisterListener(Action<bool, Transform> action)
	{
		OnEventRaised += action;
	}

	public void RemoveListener(Action<bool, Transform> action)
	{
		OnEventRaised -= action;
	}
}
