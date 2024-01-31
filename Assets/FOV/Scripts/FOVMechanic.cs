using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Pathfinding;

public class FOVMechanic : MonoBehaviour
{

	[Header("FOV Settings")]
	public LayerMask obstacleMask;
	public LayerMask playerMask;
	public float viewRadius = 5.0f;
	public float viewAngle = 135.0f;
	public Color patrolColor = new Color(0.0f, 0.0f, 0.0f, 150.0f);
	public Color spottedColor = new Color(255.0f, 0.0f, 0.0f, 150.0f);
	public bool playerBlockView;
	public AIDestinationSetter aIDestinationSetter;

	[Header("Events")]
	public UnityEvent OnSpottedEnter;
	public UnityEvent OnSpottedStay;
	public UnityEvent OnSpottedExit;

	public delegate void SpottedPlayer();
	public SpottedPlayer OnSpottedPlayer;

	private GameObject spottedPlayer;
	private Collider2D[] playerInRadius;
	private Material material;
	private Transform visiblePlayer;
	private bool previousSpotted;
	private Transform lastSpottedTransform;
	public GameObject fovChild;

	public GameObject _spottedPlayer { get { return spottedPlayer; } }

	//Creating a child object with the FOVGraphic script to actually display the FOV and getting a refrence to the material
	private void Awake()
	{
		fovChild = Instantiate(Resources.Load("FOV") as GameObject, transform);
		material = fovChild.GetComponent<MeshRenderer>().material;
	}

	//Calling the EventLogic methode every frame
	private void Update()
	{
		EventLogic();
	}

	//All of the logic for the events and for the FOV colors
	private void EventLogic()
	{
		if (spottedPlayer != null)
		{
			aIDestinationSetter.target = spottedPlayer.transform;
			aIDestinationSetter.useTargetVector = false;
			lastSpottedTransform = spottedPlayer.transform;

			material.color = spottedColor;

			if (!previousSpotted)
				OnSpottedEnter.Invoke();

			previousSpotted = true;

			OnSpottedStay.Invoke();
		}
		else
		{
			if (lastSpottedTransform != null) aIDestinationSetter.targetVector = lastSpottedTransform.position;
			aIDestinationSetter.useTargetVector = true;

			material.color = patrolColor;

			if (previousSpotted)
				OnSpottedExit.Invoke();

			previousSpotted = false;
		}
	}

	//Calculating a direction from an angle while keeping the rotation in mind
	public Vector2 DirFromAngle(float angleDeg)
	{
		angleDeg += transform.eulerAngles.z;
		return new Vector2(Mathf.Cos(angleDeg * Mathf.Deg2Rad), Mathf.Sin(angleDeg * Mathf.Deg2Rad));
	}

	//Invoking the OnSpottedPlayer if the spottedPlayer state changes
	public void UpdatePlayer(GameObject spottedPlayer)
	{
		this.spottedPlayer = spottedPlayer;

		if (OnSpottedPlayer != null)
			OnSpottedPlayer.Invoke();
	}

}
