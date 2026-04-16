using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class CharacterSelectManager : MonoBehaviour
{
    [Header("Character Data")]
    public Sprite[] characterPreviews; // preview images
    public string[] characterNames;
    public RuntimeAnimatorController[] characterAnimators;

    [Header("UI")]
    public Image characterPreviewImage;
    public TextMeshProUGUI characterNameText;
    public Button leftArrow;
    public Button rightArrow;
    public Button selectButton;

    private int currentIndex = 0;

    void Start()
    {
        UpdateDisplay();
    }

    public void NextCharacter()
    {
        currentIndex = (currentIndex + 1) % characterPreviews.Length;
        UpdateDisplay();
        Debug.Log(characterNames[currentIndex]);
    }

    public void PreviousCharacter()
    {
        currentIndex--;
        if (currentIndex < 0)
            currentIndex = characterPreviews.Length - 1;
        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        characterPreviewImage.sprite = characterPreviews[currentIndex];
        characterNameText.text = characterNames[currentIndex];
    }

    public void SelectCharacter()
    {
        // Save the selected character index
        PlayerPrefs.SetInt("SelectedCharacter", currentIndex);
        PlayerPrefs.Save();

        // Load the game
        SceneManager.LoadScene(2);
    }
}