using UnityEngine;
using UnityEngine.EventSystems;

public class ThrowTorch : MonoBehaviour
{
	[SerializeField] private GameObject torch;
	[SerializeField] private LightSourceSO throwableTorchSO;
	[SerializeField] private Transform torchHolder;
	[SerializeField] private Player player;

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			if (torchHolder == null) return;
			if (EventSystem.current.IsPointerOverGameObject()) return;
			if (!player.ConsumeItem(throwableTorchSO)) return;

			Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector3 offset = new Vector3(0, 0, 10);
			Instantiate(torch, pos + offset, Quaternion.identity, torchHolder);
		}
	}


}
