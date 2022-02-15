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
    [SerializeField] GameObject scoreMenu;

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
        Cursor.visible = false;
        Time.timeScale = 1.0f;
        instance = this;

        scoresHandler = GetComponent<ScoresHandler>();

        GetComponent<IntroOutro>().Intro();
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
        if (MusicManager.instance)
            MusicManager.instance.PlayWin();
        endMenu.GetComponentInChildren<Text>().text = "Victory";

        EndGame();
    }

    public void Lose()
    {
        if (MusicManager.instance)
            MusicManager.instance.PlayLose();
        endMenu.GetComponentInChildren<Text>().text = "Defeat";

        EndGame();
    }

    void EndGame()
    {
        paused = true;
        pausable = false;

        GetComponent<IntroOutro>().Outro();

        //If the score is a high score, ask for the name
        if (scoresHandler.ScoreCanBeInserted(score))
            AskForName();

        //show highest scores
        Cursor.visible = true;
        scoreMenu.SetActive(true);
        scoreMenu.GetComponent<ScoreMenu>().InitScores(score, scoresHandler.Read());
    }

    void AskForName()
    {
        scoreMenu.transform.Find("Next").GetComponent<AutoSelect>().enabled = false;
        scoreMenu.transform.Find("InputField").gameObject.SetActive(true);
    }

    //Called when the 'next' button is pressed on the high score screen
    public void TakeName()
    {
        if (scoresHandler.ScoreCanBeInserted(score))
        {
            string name = scoreMenu.transform.Find("InputField").Find("Text").GetComponent<Text>().text;
            Score scoreToInsert = new Score();
            scoreToInsert.score = score;
            scoreToInsert.name = name;
            scoresHandler.InsertScore(scoreToInsert);
        }
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
