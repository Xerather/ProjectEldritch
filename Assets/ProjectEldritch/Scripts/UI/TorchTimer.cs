using UnityEngine;
using UnityEngine.UI;

public class TorchTimer : MonoBehaviour
{
	private Image timerBar;
	private float currentTimer;
	private float maxTimer;
	private bool isSet = false;
	// Start is called before the first frame update
	void Start()
	{
		timerBar = GetComponent<Image>();
	}

	public void SetTimer(float targetTimer)
	{
		maxTimer = targetTimer;
		currentTimer = targetTimer;
		isSet = true;
	}

	// Update is called once per frame
	void Update()
	{
		if (!isSet) return;

		currentTimer -= Time.deltaTime;
		timerBar.fillAmount = currentTimer / maxTimer;

		if (currentTimer < 0) Destroy(this.gameObject);
	}
}
