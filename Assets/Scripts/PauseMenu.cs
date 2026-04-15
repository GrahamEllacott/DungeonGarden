using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool isPaused = false;
    public GameObject canvas;
    public GameObject helpMenu;

    bool helpIsOpen = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (helpIsOpen)
            {
                CloseHelp();
            }
            else
            {
                if (isPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }
    }

    public void Resume()
    {
        if (!helpIsOpen)
		{
			canvas.SetActive(false);
			isPaused = false;
			Time.timeScale = 1f;
		}
    }

	void Pause()
	{
		canvas.SetActive(true);
		isPaused = true;
		Time.timeScale = 0f;
	}

    public void OpenHelp()
    {
        helpMenu.SetActive(true);
        helpIsOpen = true;
    }

    public void CloseHelp()
    {
        helpMenu.SetActive(false);
        helpIsOpen = false;
    }

    public void Quit()
    {
        if (!helpIsOpen)
		{
			SceneManager.LoadScene("Menu");
		}
	}
}
