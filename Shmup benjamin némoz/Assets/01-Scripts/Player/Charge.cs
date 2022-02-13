using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : MonoBehaviour
{
    [SerializeField] GameObject weak;
    [SerializeField] GameObject strong;

    //Switch out the visual
    public void Charged()
    {
        weak.SetActive(false);
        strong.SetActive(true); //Play a sound when set active
    }

    public void Release()
    {
        Destroy(gameObject);
    }
}
