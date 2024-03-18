using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters : MonoBehaviour
{
	protected RendererColorChanger rendererColorChanger;
	protected Vector3 spawnPos;
	public int floorNumber;
	protected FloorLevelManager floorLevelManager;
	protected virtual void Start()
	{
		spawnPos = transform.position;
		floorLevelManager = FindObjectOfType<FloorLevelManager>();
		rendererColorChanger = GetComponentInChildren<RendererColorChanger>();

		SetLayerCollision();
		Reset();
	}

	private void Reset()
	{
		transform.position = spawnPos;

	}

	protected void CharacterDie()
	{
		PlayDeathAnimation();
		DestroySelf();
	}

	protected void PlayDeathAnimation()
	{
		//play death animation
	}

	protected void DestroySelf()
	{
		Destroy(gameObject);
	}

	protected void SetLayerCollision()
	{
		foreach (FloorLevel floorLevel in floorLevelManager.GetFloorLevelList())
		{
			// Debug.Log($"{this.name} , {floorLevel.GetLayerId()} = {floorLevel.floorNumber != floorNumber}");
			Physics2D.IgnoreLayerCollision(gameObject.layer, floorLevel.GetLayerId(), floorLevel.floorNumber != floorNumber);
		}
	}

	protected void SetLayerCollision(FloorLevel targetFloorLevel, bool isActive)
	{
		// Debug.Log($"{this.name} , {targetFloorLevel.GetLayerId()} = {isActive}");
		Physics2D.IgnoreLayerCollision(gameObject.layer, targetFloorLevel.GetLayerId(), isActive);
	}
	protected IEnumerator BlinkingRed()
	{
		rendererColorChanger.TurnRed();
		yield return new WaitForSeconds(.3f);
		rendererColorChanger.TurnDefault();
	}
}
