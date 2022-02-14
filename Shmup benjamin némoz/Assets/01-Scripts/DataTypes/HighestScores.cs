using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HighestScores
{
    public List<Score> scores = new List<Score>();
}

[System.Serializable]
public class Score
{
    public int score;
    public string name;
}