using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Text scoreText;
    [SerializeField] Text lifeText;

    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject endMenu;

    [HideInInspector]
    public bool paused = false;
    float previousTimeScale = 1.0f;
    bool pausable = true;

    //This is okay in c#, it gets destroyed normally
    public static GameManager instance = null;
    int score;

    ScoresHandler scoresHandler;


    private void Start()
    {
        Time.timeScale = 1.0f;
        instance = this;

        scoresHandler = GetComponent<ScoresHandler>();
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
        endMenu.GetComponentInChildren<Text>().text = "Victory";

        if (BackgroundEffects.instance != null)
            BackgroundEffects.instance.ChangeBackground(BackgroundEffects.Type.blu, 0.0f);

        EndGame();
    }

    public void Lose()
    {
        endMenu.GetComponentInChildren<Text>().text = "Defeat";

        if (BackgroundEffects.instance != null)
            BackgroundEffects.instance.ChangeBackground(BackgroundEffects.Type.softPurple, 0.0f);

        EndGame();
    }

    void EndGame()
    {
        Time.timeScale = 0.0f;
        paused = true;
        pausable = false;

        //TODO show highest scores

        //TODO ask for name

        //Temporary for testing
        string name = "Tester" + Random.Range(0, 100).ToString();
        Score scoreToInsert = new Score();
        scoreToInsert.score = score;
        scoreToInsert.name = name;

        if (scoresHandler.ScoreCanBeInserted(score))
            scoresHandler.InsertScore(scoreToInsert);

        endMenu.SetActive(true);
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
