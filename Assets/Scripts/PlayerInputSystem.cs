using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputSystem : MonoBehaviour
{
    private Rigidbody2D body;
    private PlayerInput playerInput;
    private PlayerInputActions playerInputActions;

    private void Awake() {
        body = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Jump.performed += Jump;
    }

    private void FixedUpdate()
    {
        Vector2 inputVector = playerInputActions.Player.Mouvement.ReadValue<Vector2>();
        var speed = 10;
        body.AddForce(new Vector2(inputVector.x * speed, 0), ForceMode2D.Force);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            body.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
        }
    }
}