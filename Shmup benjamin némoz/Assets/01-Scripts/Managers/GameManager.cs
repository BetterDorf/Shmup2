using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Text scoreText;
    [SerializeField] Text lifeText;

    [SerializeField] GameObject pauseMenu;

    [HideInInspector]
    public bool paused = false;
    float previousTimeScale = 1.0f;
    bool pausable = true;

    //This is okay in c#, it gets destroyed normally
    public static GameManager instance = null;
    int score;


    private void Start()
    {
        instance = this;
    }

    public void Pause()
    {
        if (!pausable)
            return;

        paused = !paused;

        if (paused)
        {
            previousTimeScale = Time.timeScale;
            Time.timeScale = 0.0f;

            if (pauseMenu)
                pauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = previousTimeScale;

            if (pauseMenu)
                pauseMenu.SetActive(false);
        }
    }

    public void Win()
    {
        //TODO everything
        pausable = false;
    }

    public void AddScore(int amount)
    {
        if (scoreText)
        {
            score += amount;
            scoreText.text = "Score : \n" + score.ToString();
        }
    }

    public void UpdateLifes(int newAmount)
    {
        if (scoreText)
        {
            lifeText.text = newAmount.ToString();
        }
    }
}
