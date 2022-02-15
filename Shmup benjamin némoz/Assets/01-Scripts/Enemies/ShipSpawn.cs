using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSpawn : MonoBehaviour
{
    [SerializeField] float time = 1.0f;
    [SerializeField] float randomInterval = 0.2f;

    [SerializeField] float startSize = 0.3f;

    [SerializeField] Color grey;

    // Start is called before the first frame update
    void Start()
    {
        time = Random.Range(time - randomInterval, time + randomInterval);
        StartCoroutine(Arrival());
    }

    //Animate the arrival of the ship and trigger the wakeup
    IEnumerator Arrival()
    {
        //Change out the color to the greyed out one
        SpriteRenderer sp = GetComponent<SpriteRenderer>();
        Color original = sp.color;
        sp.color = grey;

        //Smoothly change the scale up to the normal size
        Vector3 baseScale = transform.localScale;
        float startTime = time;
        do
        {
            yield return null;
            time -= Time.deltaTime;

            transform.localScale = baseScale - (baseScale * startSize * time / startTime);
        } while (time > 0.0f);

        sp.color = original;

        WakeUp();
    }

    //Enable the components that allow the ship to behave normally
    void WakeUp()
    {
        GetComponent<ShipBehaviour>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
    }
}
