using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NearDeathPoints : MonoBehaviour
{
    [SerializeField] int points = 50;
    [SerializeField] GameObject effect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyProjectile>())
        {
            if (GameManager.instance)
            {
                GameManager.instance.AddScore(points);

                GameObject instance = Instantiate(effect, collision.transform.position, Quaternion.identity);
                instance.GetComponentInChildren<Text>().text = points.ToString();
            }
        }
    }
}
