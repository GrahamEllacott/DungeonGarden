using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool isPaused = false;
    public GameObject canvas;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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

    public void Resume()
    {
        canvas.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
    }

	void Pause()
	{
		canvas.SetActive(true);
		isPaused = true;
		Time.timeScale = 0f;
	}

    public void Help()
    {

    }

    public void Quit()
    {
		SceneManager.LoadScene("Menu");
	}
}
