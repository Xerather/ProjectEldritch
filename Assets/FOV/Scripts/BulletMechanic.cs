using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletMechanic : MonoBehaviour
{

    [Header("Bullet Settings")]
    public LayerMask playerMask;
    public float speed = 5.0f;

    [Header("Events")]
    public UnityEvent OnHit;

    private Rigidbody2D rb;

    //Getting a refrence to the RigidBody2D
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //Getting a refrence to the RigidBody2D
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + (Vector2)rb.transform.up * speed * Time.fixedDeltaTime);
    }

    //Invoking the OnHit if the player has been hit and then destorying itself
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((playerMask.value & 1 << collision.transform.gameObject.layer) > 0)
        {
            OnHit.Invoke();
            Destroy(gameObject);
        }
    }

}
