using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    [SerializeField] float time = 1.0f;

    SpriteRenderer sp;

    private void Start()
    {
            StartCoroutine(DelayedDestroy());
    }

    IEnumerator DelayedDestroy()
    {
        yield return new WaitForSecondsRealtime(time);
        Destroy(gameObject);
    }
}
