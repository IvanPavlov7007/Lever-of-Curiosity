using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public GameObject winScreen, loseScreen;
    bool paused = false;
    bool gameOver = false;

    public void Win()
    {
        gameOver = true;
        winScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Lose()
    {
        gameOver = true;
        loseScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1f;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !gameOver)
        {
            paused = !paused;
            Time.timeScale = paused ? 0f : 1f;
        }
    }
}
