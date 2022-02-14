using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipDestruction : MonoBehaviour
{
    [SerializeField] GameObject destructionEffect;
    [SerializeField] int points = 100;

    public void LaunchDestructionEffect()
    {
        GameObject instance = Instantiate(destructionEffect, transform.position, Quaternion.identity);
        instance.GetComponentInChildren<Text>().text = points.ToString();

        if (GameManager.instance != null)
        {
            GameManager.instance.AddScore(points);
        }
    }
}
