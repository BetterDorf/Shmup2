using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreMenu : MonoBehaviour
{
    [SerializeField] GameObject scoreVisual;

    public void InitScores(int points, HighestScores highest)
    {
        transform.Find("YourScore").GetComponent<Text>().text = "Your Score : " + points.ToString();

        int i = 0;
        foreach (var score in highest.scores)
        {
            GameObject scoreInstance = Instantiate(scoreVisual, scoreVisual.transform.parent);
            scoreInstance.SetActive(true);

            scoreInstance.transform.Find("Name").GetComponent<Text>().text = score.name;
            scoreInstance.transform.Find("Points").GetComponent<Text>().text = score.score.ToString();

            scoreInstance.transform.position +=
                Vector3.down * scoreInstance.GetComponent<RectTransform>().sizeDelta.y * i++;
        }
    }
}
