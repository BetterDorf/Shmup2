using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Text scoreText;

    //This is okay in c#, it gets destroyed normally
    public static GameManager instance = null;
    int score;

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
}
