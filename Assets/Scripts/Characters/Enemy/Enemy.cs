using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Characters
{
	[SerializeField] private float detectionRange;
	[Header("Debug")]
	[SerializeField] private bool debugDrawLine;
	[SerializeField] private Gradient redColor;
	[SerializeField] private Gradient greenColor;
	[SerializeField] private LineRenderer lineOfSight;
	// Start is called before the first frame update
	void Start()
	{
		Physics2D.queriesStartInColliders = false;
	}

	// Update is called once per frame
	void Update()
	{
		RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, detectionRange);
		if (hitInfo.collider != null && debugDrawLine)
		{
			Debug.DrawLine(transform.position, hitInfo.point, Color.red);
			// lineOfSight.SetPosition(1, hitInfo.point);
			// lineOfSight.colorGradient = redColor;

			if (hitInfo.collider.CompareTag("Player"))
			{
				Debug.Log("Player got hut");
			}
		}
		else
		{
			Debug.DrawLine(transform.position, hitInfo.point, Color.green);
			// lineOfSight.SetPosition(1, transform.position + transform.right * detectionRange);
			// lineOfSight.colorGradient = greenColor;
		}

		// lineOfSight.SetPosition(0, transform.position);
	}
}
