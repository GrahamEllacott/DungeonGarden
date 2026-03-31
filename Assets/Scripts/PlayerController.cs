using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float moveSpeed = 5f;

	private Rigidbody2D rb;
	private Vector2 moveInput; // Stores raw input values

	// Start is called before the first frame update
	void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

	// Update is called once per frame
	void Update()
	{
		// Get input from Horizontal and Vertical axes (WASD/Arrow Keys by default)
		moveInput.x = Input.GetAxisRaw("Horizontal");
		moveInput.y = Input.GetAxisRaw("Vertical");

		// Flip the sprite based on direction
		if (moveInput.x != 0)
		{
			if (moveInput.x < 0)
			{
				GetComponent<SpriteRenderer>().flipX = false;
			}
			else
			{
				GetComponent<SpriteRenderer>().flipX = true;
			}
		}

		// Normalize the input vector to ensure consistent speed in all directions
		moveInput.Normalize();
	}

	// FixedUpdate is called at a fixed interval (best for physics calculations)
	void FixedUpdate()
	{
		// Apply velocity to the Rigidbody2D based on input and speed
		rb.velocity = moveInput * moveSpeed;
	}
}
