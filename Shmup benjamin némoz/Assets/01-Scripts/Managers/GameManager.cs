using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Text scoreText;
    [SerializeField] Text lifeText;

    [HideInInspector]
    public bool paused = false;
    float previousTimeScale = 1.0f;

    //This is okay in c#, it gets destroyed normally
    public static GameManager instance = null;
    int score;

    public void Pause()
    {
        paused = !paused;

        if (paused)
        {
            previousTimeScale = Time.timeScale;
            Time.timeScale = 0.0f;
        }
        else
            Time.timeScale = previousTimeScale;
    }

    private void Start()
    {
        instance = this;
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
