using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RendererColorChanger : MonoBehaviour
{
	[SerializeField] private Color colorRed = Color.red;
	[SerializeField] private Color colorDefault;
	private SpriteRenderer spriteRenderer;
	// Start is called before the first frame update
	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		colorDefault = spriteRenderer.color;
	}

	// Update is called once per frame
	public void TurnRed()
	{
		spriteRenderer.color = colorRed;
	}

	public void TurnBlue()
	{
		spriteRenderer.color = Color.blue;
	}

	public void TurnDefault()
	{
		spriteRenderer.color = colorDefault;
	}
}
