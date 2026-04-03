using UnityEngine;
using TMPro;

public class Plant : MonoBehaviour
{
    public enum PlantState { Empty, Seeded, Sprout, Ripe, Dead }

    [Header("UI")]
    public TextMeshProUGUI statusText;

    [Header("Sprites")]
    public Sprite emptySprite;
    public Sprite seededSprite;
    public Sprite sproutSprite;
    public Sprite ripeSprite;
    public Sprite deadSprite;

    [Header("Settings")]
    public float growTime = 10f;
    public float moneyValue = 25f;

    private PlantState currentState = PlantState.Empty;
    private SpriteRenderer sr;
    private float growTimer = 0f;
    private bool isGrowing = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        UpdateSprite();
    }

    void Update()
    {
        if (isGrowing)
        {
            growTimer += Time.deltaTime;
            if (growTimer >= growTime)
            {
                AdvanceGrowth();
                growTimer = 0f;
            }
        }
    }

    // Called when player presses E nearby
    public void PlantSeed()
    {
        if (currentState == PlantState.Empty)
        {
            currentState = PlantState.Seeded;
            isGrowing = true;
            UpdateSprite();
        }
    }

    // Called when player presses Space nearby
    public void Water()
    {
        Debug.Log("Plant watered! Current state: " + currentState);
        if (currentState == PlantState.Seeded ||
            currentState == PlantState.Sprout)
        {
            // Watering speeds up growth
            growTimer += 3f;
        }
    }

    // Called when player presses Q nearby
    public bool Harvest()
    {
        if (currentState == PlantState.Ripe)
        {
            currentState = PlantState.Empty;
            isGrowing = false;
            UpdateSprite();
            return true; // tells caller to add money
        }
        return false;
    }

    void AdvanceGrowth()
    {
        if (currentState == PlantState.Seeded)
            currentState = PlantState.Sprout;
        else if (currentState == PlantState.Sprout)
            currentState = PlantState.Ripe;

        UpdateSprite();
    }

    void UpdateSprite()
    {
        switch (currentState)
        {
            case PlantState.Empty:
                sr.sprite = emptySprite; break;
            case PlantState.Seeded:
                sr.sprite = seededSprite; break;
            case PlantState.Sprout:
                sr.sprite = sproutSprite; break;
            case PlantState.Ripe:
                sr.sprite = ripeSprite; break;
            case PlantState.Dead:
                sr.sprite = deadSprite; break;
        }
        // update status text
        if (statusText != null)
        {
            switch (currentState)
            {
                case PlantState.Empty:
                    statusText.text = ""; break;
                case PlantState.Seeded:
                    statusText.text = "Seeded"; break;
                case PlantState.Sprout:
                    statusText.text = "Sprout"; break;
                case PlantState.Ripe:
                    statusText.text = "Ripe!"; break;
                case PlantState.Dead:
                    statusText.text = "Dead"; break;
            }
        }

    }

    public PlantState GetState() { return currentState; }
}