using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 1.0f;

    Rigidbody2D rb;
    PlayerInput playerInput;

    Transform visual;
    [SerializeField] float rotationWhenTurning = 15.0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        playerInput = GetComponent<PlayerInput>();

        visual = GetComponentInChildren<SpriteRenderer>().transform;
    }

    private void Update()
    {
        if (GameManager.instance)
            if (GameManager.instance.paused)
                return;

        float turnValue = playerInput.actions.FindAction("Strafe").ReadValue<float>();
        rb.velocity = new Vector2(speed * turnValue, 0);

        visual.rotation = Quaternion.Euler(-Vector3.forward * rotationWhenTurning * turnValue);
    }
}
