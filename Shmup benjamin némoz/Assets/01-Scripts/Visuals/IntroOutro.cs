using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IntroOutro : MonoBehaviour
{
    [SerializeField] GameObject ui;
    [SerializeField] GameObject bounds;
    [SerializeField] GameObject levelManager;

    bool boundCompleted = false;

    // Start is called before the first frame update
    public void Intro()
    {
        //Activate the elements if the introManager doesn't know about bounds (and thus can't complete the intro)
        if (!bounds)
        {
            if (ui)
                ui.SetActive(true);
            if (levelManager)
                levelManager.SetActive(true);
        }
        else
        {
            foreach (Bounds bound in bounds.GetComponentsInChildren<Bounds>())
            {
                bound.Intro(FinishedIntro);
            }
        }
    }

    public void Outro()
    {
        //Had I more time, everything would work in reverse, they would check on this if the game was over and
        //adjust their behaviour accordingly instead of being made to stop from this.
        
        //Stop the enemies from moving
        levelManager.GetComponent<LevelManager>().StopAllEnemies();
        levelManager.GetComponent<LevelManager>().enabled = false;

        //Stop the player from playing
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<PlayerActions>().enabled = false;
        player.GetComponent<PlayerHealth>().enabled = false;
        player.GetComponent<Collider2D>().enabled = false;

        if (BackgroundEffects.instance != null)
            BackgroundEffects.instance.ChangeBackground(BackgroundEffects.Type.blu, 0.0f);

        //Remove the ui
        if (ui)
            ui.SetActive(false);

        //Slide back the bounds
        foreach (Bounds bound in bounds.GetComponentsInChildren<Bounds>())
        {
            bound.Outro();
        }

    }

    //Called back by the bounds when they are done being animated
    public void FinishedIntro()
    {
        if (boundCompleted)
            return;

        boundCompleted = true;

        StartCoroutine(EnableGameObjectAfterWait(levelManager, 0.4f));
        StartCoroutine(EnableGameObjectAfterWait(ui, 0.2f));
    }

    IEnumerator EnableGameObjectAfterWait (GameObject gameObject, float time)
    {
        yield return new WaitForSecondsRealtime(time);

        if (gameObject)
            gameObject.SetActive(true);
    }
}
