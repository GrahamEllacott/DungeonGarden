using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("HUD Elements")]
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI seedsText;
    public TextMeshProUGUI phaseText;

    [Header("Game Settings")]
    public float dayDuration = 60f;
    public float nightDuration = 60f;
    public float moneyRequired = 200f;
    public int startingSeeds = 10;

    [Header("Backgrounds")]
    public GameObject dayBackground;
    public GameObject nightBackground;

    [Header("UI Panels")]
    public GameObject winPanel;
    public GameObject losePanel;

    // Tracked values
    private float money = 0f;
    private int seeds;
    private float timeRemaining;
    private bool isDay = true;
    private bool gameActive = true;

    void Start()
    {
        seeds = startingSeeds;
        timeRemaining = dayDuration;
        UpdateHUD();

        // Make sure panels are hidden at start
        winPanel.SetActive(false);
        losePanel.SetActive(false);
    }

    void Update()
    {
        if (!gameActive) return;

        // Count down timer
        timeRemaining -= Time.deltaTime;

        // Check for phase switch
        if (isDay && timeRemaining <= 0)
        {
            SwitchToNight();
        }
        else if (!isDay && timeRemaining <= 0)
        {
            EndGame();
        }

        UpdateHUD();
    }

    void SwitchToNight()
    {
        isDay = false;
        timeRemaining = nightDuration;
        dayBackground.SetActive(false);
        nightBackground.SetActive(true);
        phaseText.text = "NIGHT";
        phaseText.color = new Color(0.6f, 0.4f, 1f); // purple
    }

    void EndGame()
    {
        gameActive = false;
        if (money >= moneyRequired)
        {
            winPanel.SetActive(true);
        }
        else
        {
            losePanel.SetActive(true);
        }
    }

    void UpdateHUD()
    {
        moneyText.text = "$" + money.ToString("F2");
        timerText.text = Mathf.CeilToInt(timeRemaining).ToString();
        seedsText.text = "Seeds: " + seeds;
        phaseText.text = isDay ? "DAY" : "NIGHT";
    }

    // Call this from other scripts when player earns money
    public void AddMoney(float amount)
    {
        money += amount;
    }

    // Call this when player uses a seed
    public void UseSeeds(int amount)
    {
        seeds -= amount;
    }

    public bool HasSeeds()
    {
        return seeds > 0;
    }
}