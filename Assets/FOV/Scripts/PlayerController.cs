using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{

    [Header("Movement Settings")]
    public float movementSpeed = 5.0f;
    public float rotationSpeed = 10.0f;

    private Camera main;
    private Rigidbody2D rb;
    private Vector2 mousePosition;
    private Vector2 moveDirection;
    private float angle;

    //Getting refrences to the camera and rigidbody
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        main = Camera.main;
    }

    //Rotating the player and capturing the input every frame
    private void Update()
    {
        CaptureInput();
    }

    //Capturing keyboard input so we can translate it into movement
    private void CaptureInput()
    {
        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    //Moving and rotating the player
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveDirection * movementSpeed * Time.fixedDeltaTime);

        if (Vector2.Distance(main.ScreenToWorldPoint(Input.mousePosition), transform.position) > 0.5f)
        {
            mousePosition = main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
            rb.MoveRotation(Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), rotationSpeed * Time.fixedDeltaTime));
        }
    }

}
