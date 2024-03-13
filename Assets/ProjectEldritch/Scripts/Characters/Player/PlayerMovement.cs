using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private Player player;
	[SerializeField] private float rotationSpeed;
	[Header("Debug")]
	[SerializeField] private Vector2 moveDirection;
	private PlayerStats playerStats => player.playerStats;
	public Rigidbody2D rb;
	public float moveSpeed => playerStats.speed;
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		MovementInputs();
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
		float moveX = Input.GetAxisRaw("Horizontal");
		float moveY = Input.GetAxisRaw("Vertical");

		moveDirection = new Vector2(moveX, moveY).normalized;
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
		rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
	}
}
