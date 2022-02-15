using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusStar : MonoBehaviour
{
    [SerializeField] int points = 500;
    [SerializeField] float speed = 1.0f;

    [SerializeField] GameObject pointsPopUp;

    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.down * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>())
        {
            if (GameManager.instance)
                GameManager.instance.AddScore(points);

            GameObject instance = Instantiate(pointsPopUp, transform.position, Quaternion.identity);
            instance.GetComponentInChildren<Text>().text = points.ToString();

            Destroy(gameObject);
        }
    }
}
