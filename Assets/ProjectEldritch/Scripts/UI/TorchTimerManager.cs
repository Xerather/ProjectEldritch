using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchTimerManager : MonoBehaviour
{
	[SerializeField] private TorchTimer torchTimerPrefab;
	[SerializeField] private TorchEventChannelSO onTorchSpawnChannel;

	void OnEnable()
	{
		onTorchSpawnChannel.RegisterListener(SpawnTorchTimer);
	}
	void OnDisable()
	{
		onTorchSpawnChannel.RemoveListener(SpawnTorchTimer);
	}

	private void SpawnTorchTimer(float torchTime, Transform target)
	{
		TorchTimer torchTimer = Instantiate(torchTimerPrefab, transform);
		torchTimer.SetTimer(torchTime);
		transform.position = Camera.main.WorldToScreenPoint(target.position);
	}
}
