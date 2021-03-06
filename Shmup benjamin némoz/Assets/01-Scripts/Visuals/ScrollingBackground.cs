using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    [HideInInspector]
    public float speed = 1.0f;
    float realSpeed = 0.0f;

    [HideInInspector]
    public float timeToMaxSpeed = 10.0f;

    SpriteRenderer sp;

    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (realSpeed > speed || timeToMaxSpeed <= 0.0f)
            realSpeed = speed;
        else
            realSpeed += Time.deltaTime * speed / timeToMaxSpeed;

        transform.position += Vector3.down * realSpeed * Time.deltaTime;
    }

    private void OnBecameInvisible()
    {
        transform.position += Vector3.up * sp.sprite.bounds.size.y * transform.localScale.y * 2;
    }
}
