using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Events/Item Event Channel")]
public class ItemEventChannelSO : ScriptableObject
{
	private Action<ItemSO, int> OnEventRaised;

	public void RaiseEvent(ItemSO itemSO, int value)
	{
		OnEventRaised?.Invoke(itemSO, value);
	}

	public void RegisterListener(Action<ItemSO, int> action)
	{
		OnEventRaised += action;
	}

	public void RemoveListener(Action<ItemSO, int> action)
	{
		OnEventRaised -= action;
	}
}
