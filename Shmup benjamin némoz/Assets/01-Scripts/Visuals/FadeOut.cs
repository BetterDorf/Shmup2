using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    [SerializeField] float time = 1.0f;

    SpriteRenderer sp;

    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();

        if (!sp)
            sp = GetComponentInChildren<SpriteRenderer>();

        if (!sp)
            StartCoroutine(DelayedDestroy());
        else
            StartCoroutine(FadeOutOverTime());
    }

    IEnumerator DelayedDestroy()
    {
        yield return new WaitForSecondsRealtime(time);
        Destroy(gameObject);
    }

    IEnumerator FadeOutOverTime()
    {
        do
        {
            sp.color = new Color(sp.color.r, sp.color.b, sp.color.g, sp.color.a - (1 / time) * Time.deltaTime);
            yield return null;
        } while (sp.color.a > 0.05f);
    }
}
