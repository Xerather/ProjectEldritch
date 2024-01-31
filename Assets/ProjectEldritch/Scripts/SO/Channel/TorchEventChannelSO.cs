using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Events/Torch Event Channel")]
public class TorchEventChannelSO : ScriptableObject
{
	private Action<float, Transform> OnEventRaised;

	public void RaiseEvent(float timer, Transform transform)
	{
		OnEventRaised?.Invoke(timer, transform);
	}

	public void RegisterListener(Action<float, Transform> action)
	{
		OnEventRaised += action;
	}

	public void RemoveListener(Action<float, Transform> action)
	{
		OnEventRaised -= action;
	}
}
