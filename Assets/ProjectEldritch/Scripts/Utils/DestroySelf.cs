using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
	public float destroyTimer = 5f;
	private float timeElapsed = 0f;
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		timeElapsed += Time.deltaTime;
		if (timeElapsed > destroyTimer)
			Destroy(this.gameObject);
	}
}
