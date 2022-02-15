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

        foreach (Bounds bound in bounds.GetComponentsInChildren<Bounds>())
        {
            bound.Intro(FinishedIntro);
        }
    }

    public void Outro()
    {
        //Remove the ui
        if (ui)
            ui.SetActive(false);

        //Slide back the bounds
        foreach (Bounds bound in bounds.GetComponentsInChildren<Bounds>())
        {
            bound.Outro();
        }
    }

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
