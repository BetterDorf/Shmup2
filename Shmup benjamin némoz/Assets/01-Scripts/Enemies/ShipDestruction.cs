using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipDestruction : MonoBehaviour
{
    [SerializeField] GameObject destructionEffect;

    [SerializeField] GameObject silverStar;
    [SerializeField] GameObject goldStar;
    [SerializeField] float starOdds = 0.2f;

    [SerializeField] int points = 100;

    static int silverFailed = 0;
    static int goldFailed = 0;
    float nudgeEffect = 0.1f;

    public void LaunchDestructionEffect()
    {
        GameObject instance = Instantiate(destructionEffect, transform.position, Quaternion.identity);
        instance.GetComponentInChildren<Text>().text = points.ToString();

        if (GameManager.instance != null)
        {
            GameManager.instance.AddScore(points);
        }

        //Check wether to spawn a star
        if (Random.Range(0.0f, 1.0f) <= starOdds + nudgeEffect * silverFailed)
        {
            silverFailed = 0;

            //Check to make a gold star
            if (Random.Range(0.0f, 1.0f) <= starOdds + nudgeEffect * goldFailed)
            {
                goldFailed = 0;
                Instantiate(goldStar, transform.position, Quaternion.identity);
            }
            else
            {
                goldFailed++;
                Instantiate(silverStar, transform.position, Quaternion.identity);
            }
        }
        else
        {
            silverFailed++;
        }
    }
}
