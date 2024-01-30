using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private float rotationSpeed;
	[SerializeField] private Rigidbody2D rb;
	[Header("Debug")]
	[SerializeField] private Vector2 moveDirection;
	[SerializeField] private Light2D playerLight;
	[Header("Light parameter")]
	[SerializeField] private float onInnerRadius;
	[SerializeField] private float onOuterRadius;
	[SerializeField] private float offInnerRadius, offOuterRadius;

	public float moveSpeed;
	private bool lightOn = true;
	public bool isPlayerVisible => lightOn;
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		MovementInputs();

		if (Input.GetKeyDown(KeyCode.F))
		{
			lightOn = !lightOn;

			playerLight.pointLightInnerRadius = lightOn ? onInnerRadius : offInnerRadius;
			playerLight.pointLightOuterRadius = lightOn ? onOuterRadius : offOuterRadius;
		}

		if (Input.GetKeyDown(KeyCode.Mouse0))
		{

		}
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
