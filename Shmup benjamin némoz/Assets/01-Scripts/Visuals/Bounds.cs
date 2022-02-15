using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class Bounds : MonoBehaviour
{
    [SerializeField] float slidingSpeed = 1.2f;
    [SerializeField] float offCameraDistance = 5.0f;
    int sliding = 0; //0 not moving, 1 is outward, 2 is inward

    Action finishedIntroAction;

    float margin = 0.05f;
    float startingPos;

    private void Awake()
    {
        startingPos = transform.position.x;
    }

    private void Update()
    {
        if (sliding == 1)
        {
            transform.position = new Vector3(transform.position.x + slidingSpeed * Time.deltaTime,
                transform.position.y, transform.position.z);

            if (Mathf.Abs(transform.position.x - startingPos) < margin ||
                Mathf.Abs(transform.position.x) < Mathf.Abs(startingPos))
            {
                sliding = 0;
                transform.position = new Vector3(startingPos, transform.position.y, transform.position.z);

                finishedIntroAction();
            }
        }
        else if (sliding == 2)
        {
            transform.position = new Vector3(transform.position.x - slidingSpeed * Time.deltaTime,
                transform.position.y, transform.position.z);
        }
    }

    //Play the intro and launch a given function
    public void Intro(Action action)
    {
        finishedIntroAction = action;

        //Move the bounds out of the screen
        transform.position = new Vector3(transform.position.x - Mathf.Sign(slidingSpeed) * offCameraDistance,
            transform.position.y, transform.position.z);

        sliding = 1;
    }

    public void Outro()
    {
        sliding = 2;
    }

    private void OnBecameInvisible()
    {
        sliding = 0;
    }
}
