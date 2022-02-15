using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScoresHandler : MonoBehaviour
{
    [SerializeField] string path;
    [SerializeField] int scoresRecorded = 10;

    private void Start()
    {
        path = Application.persistentDataPath + path;

        //Create the highscores file if it doesn't already exist
        if (!File.Exists(path))
        {
            Write(new HighestScores());
        }
    }

    public void Write(HighestScores highestScores)
    {
        string jsonFormat = JsonUtility.ToJson(highestScores);

        StreamWriter writer = new StreamWriter(path, false);
        writer.Write(jsonFormat);

        //Update the resource so that the game has knowledge of it
        Resources.Load(path);

        writer.Close();
    }

    public HighestScores Read()
    {
        StreamReader reader = new StreamReader(path);
        string jsonFormat = reader.ReadToEnd();

        reader.Close();

        return JsonUtility.FromJson<HighestScores>(jsonFormat);
    }

    public bool ScoreCanBeInserted(int score)
    {
        HighestScores highestScores = Read();

        if (scoresRecorded > highestScores.scores.Count)
            return true;

        //The list will always be sorted, so we only check the last, smallest score
        if (score > highestScores.scores[highestScores.scores.Count - 1].score)
            return true;

        return false;
    }

    public void InsertScore(Score score)
    {
        HighestScores highest = Read();

        //Find a spot for the score and add it
        bool placeFound = false;
        for (int i = 0; i < highest.scores.Count; i++)
        {
            if (score.score > highest.scores[i].score)
            {
                highest.scores.Insert(i, score);
                placeFound = true;
                break;
            }
        }

        //Add the score at the end if it would be the smallest
        if (!placeFound)
            highest.scores.Add(score);

        //Remove the last score if there are too many scores
        if (scoresRecorded < highest.scores.Count)
            highest.scores.RemoveAt(highest.scores.Count - 1);

        //Write the scores
        Write(highest);
    }

    public void ResetScores()
    {
        Write(new HighestScores());
    }
}
