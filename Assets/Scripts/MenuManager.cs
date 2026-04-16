using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
	public GameObject helpMenu;
	public GameObject aboutScreen;

	bool helpIsOpen = false;
	bool aboutIsOpen = false;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			CloseAbout();
			CloseHelp();
		}
	}

	// Start is called before the first frame update
	public void StartGame()
	{
		if (!helpIsOpen && !aboutIsOpen)
		{
            //SceneManager.LoadScene("Level 1");
            SceneManager.LoadScene("CharacterSelect");
        }
	}

	public void Quit()
	{
		if (!helpIsOpen && !aboutIsOpen)
		{
			Application.Quit();
		}
	}

	public void OpenHelp()
	{
		if (!aboutIsOpen)
		{
			helpMenu.SetActive(true);
			helpIsOpen = true;
		}
	}

	public void CloseHelp()
	{
		helpMenu.SetActive(false);
		helpIsOpen = false;
	}

	public void OpenAbout()
	{
		if (!helpIsOpen)
		{
			aboutScreen.SetActive(true);
			aboutIsOpen = true;
		}
	}

	public void CloseAbout()
	{
		aboutScreen.SetActive(false);
		aboutIsOpen = false;
	}
}

