using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private Player player;
	[SerializeField] private float rotationSpeed;
	[Header("Debug")]
	[SerializeField] public Vector2 moveDirection;
	[SerializeField] private Vector3 mousePosition;
	public Rigidbody2D playerRb;
	[SerializeField] public Transform faceDirection;
	private PlayerStats playerStats => player.playerStats;
	private float moveSpeed;

	//Dash
	[SerializeField] private float dashCooldown = 0;
	[SerializeField] private float dashDurationCounter = 0;
	[SerializeField] private float dashCounter;
	[SerializeField] private TrailRenderer trailRenderer;
	// Start is called before the first frame update
	void Start()
	{
		trailRenderer.emitting = false;
		moveSpeed = playerStats.speed;
		dashCounter = playerStats.maxDashCounter;
	}

	void Update()
	{
		MovementInputs();
		Dash();


		mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}

	void FixedUpdate()
	{
		RotateDirection();
	}

	void LateUpdate()
	{
		Move();
	}

	private void SpawnAtMousePos()
	{
	}

	private void MovementInputs()
	{
		moveDirection.x = Input.GetAxisRaw("Horizontal");
		moveDirection.y = Input.GetAxisRaw("Vertical");

		moveDirection.Normalize();
	}
	private void Move()
	{
		playerRb.velocity = moveDirection * moveSpeed;
	}

	private void Dash()
	{
		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			if (dashDurationCounter <= 0 && dashCounter > 0)
			{
				moveSpeed = playerStats.dashSpeed;
				dashDurationCounter = PlayerStats.DASH_DURATION;
				dashCounter--;
			}
		}

		if (dashDurationCounter > 0)
		{
			dashDurationCounter -= Time.deltaTime;
			trailRenderer.emitting = true;
			if (dashDurationCounter <= 0)
			{
				trailRenderer.emitting = false;
				moveSpeed = playerStats.speed;
			}
		}

		if (dashCooldown > 0)
		{
			dashCooldown -= Time.deltaTime;
		}
		else
		{
			if (dashCounter < playerStats.maxDashCounter) dashCounter++;
		}

		if (dashCounter < playerStats.maxDashCounter)
		{
			dashCooldown = playerStats.dashCooldown;
		}
	}

	private void RotateDirection()
	{
		Vector3 direction = mousePosition - player.transform.position;

		float aimAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		faceDirection.rotation = Quaternion.Euler(0, 0, aimAngle);

		if (moveDirection != Vector2.zero)
		{
			// Quaternion targetRotation = Quaternion.LookRotation(transform.forward, moveDirection);
			// Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

			// faceDirection.right = moveDirection;
		}
	}
}
