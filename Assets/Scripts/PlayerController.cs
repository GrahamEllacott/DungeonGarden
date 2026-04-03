using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float moveSpeed = 5f;

    [Header("Screen Bounds")]
    public float minX = -8f;
    public float maxX = 8f;
    public float minY = -4.5f;
    public float maxY = 3.5f;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 moveInput; // Stores raw input values
    private bool isWatering = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        animator = GetComponent<Animator>();
    }

	// Update is called once per frame
	void Update()
	{
        // Block movement while watering
        if (isWatering) return;

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

        // Send movement to Animator
        bool isMoving = moveInput.magnitude > 0.1f;
        animator.SetBool("isMoving", isMoving);
        animator.SetFloat("moveX", moveInput.x);

        // Water spray on Space (this may be redundant now TODO: check)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Water nearby plants
            Collider2D[] nearby = Physics2D.OverlapCircleAll(
                transform.position, 1.5f);
            foreach (Collider2D col in nearby)
            {
                Plant plant = col.GetComponent<Plant>();
                if (plant != null)
                {
                    plant.Water();
                    Debug.Log("Watered plant!");
                }
            }
            StartCoroutine(WaterSpray());

            // Spray nearby bunnies
            Collider2D[] allNearby = Physics2D.OverlapCircleAll(
                transform.position, 1.5f);
            foreach (Collider2D col in allNearby)
            {
                BunnyController bunny = col.GetComponent<BunnyController>();
                if (bunny != null)
                {
                    bunny.GetSprayed();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
            TryInteractWithPlant("plant");

        if (Input.GetKeyDown(KeyCode.Q))
            TryInteractWithPlant("harvest");


        // Plant interactions
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteractWithPlant("plant");
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TryInteractWithPlant("harvest");
        }
    }

	// FixedUpdate is called at a fixed interval (best for physics calculations)
	void FixedUpdate()
	{
        if (isWatering)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        // Apply velocity to the Rigidbody2D based on input and speed
        rb.velocity = moveInput * moveSpeed;

        // Clamp position to screen bounds
        Vector2 clampedPos = rb.position;
        clampedPos.x = Mathf.Clamp(clampedPos.x, minX, maxX);
        clampedPos.y = Mathf.Clamp(clampedPos.y, minY, maxY);
        rb.position = clampedPos;
    }

    private IEnumerator WaterSpray()
    {
        isWatering = true;
        animator.SetBool("isWatering", true);
        animator.SetBool("isMoving", false);

        // Change loopCount to alter how many sprays
        int loopCount = 3;
        float clipLength = 0.15f; // match this to water_spray clip length
        yield return new WaitForSeconds(clipLength * loopCount);

        isWatering = false;
        animator.SetBool("isWatering", false);
    }

    private void TryInteractWithPlant(string action)
    {
        // Detect plants nearby
        Collider2D[] nearby = Physics2D.OverlapCircleAll(
            transform.position, 1.5f);

        foreach (Collider2D col in nearby)
        {
            Plant plant = col.GetComponent<Plant>();
            if (plant != null)
            {
                if (action == "plant")
                {
                    plant.PlantSeed();
                    // Use a seed
                    FindObjectOfType<GameManager>().UseSeeds(1);
                }
                else if (action == "harvest")
                {
                    bool harvested = plant.Harvest();
                    if (harvested)
                    {
                        FindObjectOfType<GameManager>()
                            .AddMoney(plant.moneyValue);
                    }
                }
            }
        }
    }
}

