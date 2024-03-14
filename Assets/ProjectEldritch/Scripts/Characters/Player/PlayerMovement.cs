using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private Player player;
	[SerializeField] private float rotationSpeed;
	[Header("Debug")]
	[SerializeField] private Vector2 moveDirection;
	public Rigidbody2D rb;
	private PlayerStats playerStats => player.playerStats;
	private float moveSpeed;

	//Dash
	[SerializeField] private float dashCooldown = 0;
	[SerializeField] private float dashDurationCounter = 0;
	[SerializeField] private float dashCounter;
	[SerializeField] private TrailRenderer tr;
	// Start is called before the first frame update
	void Start()
	{
		tr.emitting = false;
		moveSpeed = playerStats.speed;
		dashCounter = playerStats.maxDashCounter;
	}

	// Update is called once per frame
	void Update()
	{
		MovementInputs();
		Dash();
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

	private void Dash()
	{
		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			if (dashDurationCounter <= 0 && dashCounter > 0)
			{
				moveSpeed = PlayerStats.DASH_SPEED;
				dashDurationCounter = PlayerStats.DASH_DURATION;
				dashCounter--;
			}
		}

		if (dashDurationCounter > 0)
		{
			dashDurationCounter -= Time.deltaTime;
			tr.emitting = true;
			if (dashDurationCounter <= 0)
			{
				tr.emitting = false;
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
		if (moveDirection != Vector2.zero)
		{
			Quaternion targetRotation = Quaternion.LookRotation(transform.forward, moveDirection);
			Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

			rb.MoveRotation(rotation);
		}
	}

	private void Move()
	{
		rb.velocity = moveDirection * moveSpeed;
	}
}
