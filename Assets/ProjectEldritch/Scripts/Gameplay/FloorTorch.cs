using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTorch : MonoBehaviour
{
	[SerializeField] private LightSourceSO torch;
	[SerializeField] private TorchEventChannelSO onTorchSpawnChannel;
	private float timeElapsed;

	void Start()
	{
		timeElapsed = 0;
		onTorchSpawnChannel.RaiseEvent(torch.lightDuration, transform);
	}

	// Update is called once per frame
	void Update()
	{
		timeElapsed += Time.deltaTime;
		if (timeElapsed > torch.lightDuration)
		{
			Destroy(this.gameObject);
		}
	}
}
