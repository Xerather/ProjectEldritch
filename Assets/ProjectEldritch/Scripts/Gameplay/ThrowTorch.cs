using UnityEngine;

public class ThrowTorch : MonoBehaviour
{
	[SerializeField] private GameObject torch;
	[SerializeField] private Transform torchHolder;

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector3 offset = new Vector3(0, 0, 10);
			Instantiate(torch, pos + offset, Quaternion.identity, torchHolder);
		}
	}
}
