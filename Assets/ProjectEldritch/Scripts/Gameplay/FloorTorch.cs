using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTorch : MonoBehaviour
{
	[SerializeField] LightSourceSO torch;
	private float timeElapsed;

	void Start()
	{
		timeElapsed = 0;
	}

	// Update is called once per frame
	void Update()
	{
		timeElapsed += Time.deltaTime;
		if (timeElapsed > torch.lightDuration)
		{
			// Debug.Log($"{this.name} is destroyed");
			Destroy(this.gameObject);
		}
	}
}
