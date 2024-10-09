using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputSystem : MonoBehaviour
{
    private Rigidbody2D body;
    private PlayerInput playerInput;
    private PlayerInputActions playerInputActions;
    private bool isGrounded;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Move.performed += Jump;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Tiles")
        {
            isGrounded = true;
        }
    }

    private void FixedUpdate()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        var speed = 5;
        body.AddForce(inputVector * speed, ForceMode2D.Force);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            isGrounded = false;
        }
        if (context.phase == InputActionPhase.Performed && !isGrounded)
        {
            Debug.Log("Jump" + context.phase);
            body.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
        }
    }
}
