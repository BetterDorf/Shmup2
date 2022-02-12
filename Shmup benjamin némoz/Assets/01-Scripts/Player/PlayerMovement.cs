using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 1.0f;

    Rigidbody2D rb;
    PlayerInput playerInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        rb.velocity = new Vector2(speed * playerInput.actions.FindAction("Strafe").ReadValue<float>(), 0);
    }

    //public void MoveInput(InputAction.CallbackContext context)
    //{
    //    Debug.Log("Message sent");
    //    //Check what we performed with the input
    //    switch(context.phase)
    //    {
    //        case InputActionPhase.Started:
    //            speed = startSpeed * Mathf.Sign(context.ReadValue<float>());
    //            Debug.Log("Started");
    //            break;
    //        case InputActionPhase.Performed:
    //            speed = holdSpeed * Mathf.Sign(context.ReadValue<float>());
    //            Debug.Log("Performed");
    //            break;
    //        case InputActionPhase.Canceled:
    //            speed = 0.0f;
    //            Debug.Log("Canceled");
    //            break;
    //    }

    //    //Set our velocity
    //    rb.velocity = new Vector2(speed, 0);
    //}
}
