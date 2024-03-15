using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	[SerializeField] private float speed;
	[SerializeField] private float duration;
	[SerializeField] private SpriteRenderer spriteRenderer;
	private float destroyCounter;
	private Rigidbody2D rb;

	[SerializeField] private bool rotateAround;
	[SerializeField] private float rotateSpeed;

	[SerializeField] private List<string> UnaffectedTagList;

	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		destroyCounter = duration;
	}

	// Update is called once per frame
	void Update()
	{
		if (destroyCounter > 0)
		{
			destroyCounter -= Time.deltaTime;
		}
		else
		{
			DestroySelf();
		}
		rb.AddForce(transform.right * speed);
		if (!rotateAround) return;
		spriteRenderer.transform.Rotate(new Vector3(0, 0, rotateSpeed) * Time.deltaTime);
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (UnaffectedTagList.Contains(col.tag)) return;
		DestroySelf();
	}

	private void OnCollisionEnter2D(Collision2D col)
	{
		if (UnaffectedTagList.Contains(col.gameObject.tag)) return;
		DestroySelf();
	}

	private void DestroySelf()
	{
		Destroy(gameObject);
	}
}
