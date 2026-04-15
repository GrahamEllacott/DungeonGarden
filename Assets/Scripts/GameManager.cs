using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    //public GameObject resultsPanel;

    public TextMeshProUGUI seedsPlantedText;
    public TextMeshProUGUI moneyEarnedText;

    // Tracked values
    private float money = 0f;
    private int seeds;
    private float timeRemaining;
    private bool isDay = true;
    private bool gameActive = true;

    private int seedsPlanted = 0;

    void Start()
    {
        seeds = startingSeeds;
        timeRemaining = dayDuration;
        UpdateHUD();

        // Make sure panels are hidden at start
        winPanel.SetActive(false);
        losePanel.SetActive(false);

        // Set day colours on start
        moneyText.color = Color.black;
        timerText.color = Color.black;
        seedsText.color = Color.black;
        phaseText.color = Color.black;
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

    [Header("HUD Images")]
    public Image moneyBackground;
    public Image timerBackground;
    public Image seedsBackground;
    public Image phaseBackground;

    void SwitchToNight()
    {
        isDay = false;
        timeRemaining = nightDuration;
        dayBackground.SetActive(false);
        nightBackground.SetActive(true);

        // Update phase text
        phaseText.text = "NIGHT";
        phaseText.color = new Color(0.8f, 0.6f, 1f); // purple

        // Update other text to night colours
        moneyText.color = new Color(0.0f, 0.6f, 0.2f);
        timerText.color = new Color(0.0f, 0.6f, 0.2f);
        seedsText.color = new Color(0.0f, 0.6f, 0.2f);
        phaseText.color = new Color(0.0f, 0.6f, 0.2f);

        // Update background pill colours
        moneyBackground.color = new Color(0.2f, 0.1f, 0.3f, 0.8f);
        timerBackground.color = new Color(0.2f, 0.1f, 0.3f, 0.8f);
        seedsBackground.color = new Color(0.2f, 0.1f, 0.3f, 0.8f);
        phaseBackground.color = new Color(0.2f, 0.1f, 0.3f, 0.8f);

        // Find all plants and switch them to night mode
        Plant[] plants = FindObjectsOfType<Plant>();
        foreach (Plant plant in plants)
        {
            plant.SetNightMode(true);
        }
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
        //resultsPanel.SetActive(true);
    }

    void UpdateHUD()
    {
        moneyText.text = "$" + money.ToString("F2") + " / $" + moneyRequired.ToString("F2");
        timerText.text = Mathf.CeilToInt(timeRemaining).ToString();
        seedsText.text = "Seeds: " + seeds;
        phaseText.text = isDay ? "DAY" : "NIGHT";

        if (seedsPlantedText != null)
            seedsPlantedText.text = "Seeds Planted: " + seedsPlanted.ToString();
        if (moneyEarnedText != null)
            moneyEarnedText.text = "Money Earned: $" + money.ToString("F2");
    }

    // Call this from other scripts when player earns money
    public void AddMoney(float amount)
    {
        money += amount;
    }

    // Call this when player uses a seed
    public void UseSeeds(int amount)
    {
        if (HasSeeds(amount))
        {
            seeds -= amount;
            seedsPlanted += amount;
        }
    }


	public bool HasSeeds(int amount)
    {
        return seeds >= amount;
    }


	public void TryAgain()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

    public void Level2()
    {
        SceneManager.LoadScene("Level 2");
    }

	public void Level3()
	{
		SceneManager.LoadScene("Level 3");
	}
}
