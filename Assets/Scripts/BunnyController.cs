using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 1.5f;

    [Header("Bounds")]
    public float minX = -7.5f;
    public float maxX = 7.5f;
    public float minY = -4f;
    public float maxY = 3.5f;

    private Transform targetPlant;
    private SpriteRenderer sr;

    [Header("Settings")]
    public float damageRadius = 0.5f;
    private bool isEating = false;

    public event System.Action OnBunnyDestroyed;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        FindNearestPlant();
    }

    void Update()
    {
        if (targetPlant == null)
        {
            FindNearestPlant();
            return;
        }

        // Check if plant still exists
        if (targetPlant.gameObject == null)
        {
            targetPlant = null;
            return;
        }

        float distanceToPlant = Vector2.Distance(
       transform.position, targetPlant.position);

        // If close enough, start eating
        if (distanceToPlant < damageRadius)
        {
            if (!isEating)
            {
                StartCoroutine(EatPlant());
            }
            return;
        }


        // Move toward target plant
        Vector2 direction = (targetPlant.position -
            transform.position).normalized;
        transform.position += (Vector3)direction *
            moveSpeed * Time.deltaTime;

        // Flip sprite based on direction
        if (direction.x > 0)
            sr.flipX = true;
        else if (direction.x < 0)
            sr.flipX = false;

        // Check bounds
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;
    }

    void FindNearestPlant()
    {
        Plant[] plants = FindObjectsOfType<Plant>();
        float closestDistance = Mathf.Infinity;

        foreach (Plant plant in plants)
        {
            Debug.Log("Plant state: " + plant.GetState());

            // Only target plants that are growing
            if (plant.GetState() == Plant.PlantState.Empty ||
                plant.GetState() == Plant.PlantState.Dead)
                continue;

            float distance = Vector2.Distance(
                transform.position, plant.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                targetPlant = plant.transform;
            }
        }
    }

    private IEnumerator EatPlant()
    {
        isEating = true;
        yield return new WaitForSeconds(2f); // takes 2 seconds to eat

        if (targetPlant != null)
        {
            Plant plant = targetPlant.GetComponent<Plant>();
            if (plant != null)
            {
                plant.GetEaten(); // destroy the plant
            }
        }

        targetPlant = null;
        isEating = false;
        FindNearestPlant(); // find next target
    }

    // Called when player sprays the bunny
    public void GetSprayed()
    {
        // Bunny runs off screen
        StartCoroutine(RunAway());
    }

    private System.Collections.IEnumerator RunAway()
    {
        moveSpeed = 4f; // run fast!
        Vector2 awayDirection = (transform.position -
            FindObjectOfType<PlayerController>()
            .transform.position).normalized;

        float timer = 0f;
        while (timer < 2f)
        {
            transform.position += (Vector3)awayDirection *
                moveSpeed * Time.deltaTime;
            timer += Time.deltaTime;
            yield return null;
        }

        OnBunnyDestroyed?.Invoke();
        Destroy(gameObject);
    }
}
