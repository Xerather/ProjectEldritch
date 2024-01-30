using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Characters
{
	[SerializeField] private Rigidbody2D rb;
	[SerializeField] private Transform playerTransform;
	[SerializeField] private bool isChasing;

	void Start()
	{
		Physics2D.queriesStartInColliders = false;
	}




	// void Update()
	// {
	// RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, detectionRange);
	// if (hitInfo.collider != null && debugDrawLine)
	// {
	// 	Debug.DrawLine(transform.position, hitInfo.point, Color.red);
	// lineOfSight.SetPosition(1, hitInfo.point);
	// lineOfSight.colorGradient = redColor;

	// 	if (hitInfo.collider.CompareTag("Player"))
	// 	{
	// 		Debug.Log("Player got hut");
	// 	}
	// }
	// else
	// {
	// Debug.DrawLine(transform.position, hitInfo.point, Color.green);
	// lineOfSight.SetPosition(1, transform.position + transform.right * detectionRange);
	// lineOfSight.colorGradient = greenColor;
	// }

	// lineOfSight.SetPosition(0, transform.position);
	// }
}
