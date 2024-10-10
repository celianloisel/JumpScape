using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputSystem : MonoBehaviour
{
    private Rigidbody2D body;
    private PlayerInput playerInput;
    private PlayerInputActions playerInputActions;
    private bool isGrounded;
    private bool canJump;

    [SerializeField] private float groundCheckDistance = 0.1f; 
    [SerializeField] private LayerMask groundLayer;

    private void Awake() {
        body = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Jump.performed += Jump;
        canJump = true;
    }

    private void FixedUpdate()
    {
        Vector2 inputVector = playerInputActions.Player.Mouvement.ReadValue<Vector2>();
        var speed = 10;
        body.AddForce(new Vector2(inputVector.x * speed, 0), ForceMode2D.Force);

        isGrounded = CheckGround();

        // Si le joueur est au sol, on réactive la possibilité de sauter
        if (isGrounded && !canJump)
        {
            canJump = true;
        }

        Vector2 rayOrigin = new Vector2(transform.position.x, transform.position.y - 0.5f);
        Debug.DrawRay(rayOrigin, Vector2.down * groundCheckDistance, Color.red);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded && canJump)
        {
            body.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
            canJump = false; // Désactiver les sauts tant qu'il est en l'air
        }
    }

    private bool CheckGround()
    {
        Vector2 rayOrigin = new Vector2(transform.position.x, transform.position.y - 0.5f);
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, groundCheckDistance, groundLayer);

        if (hit.collider != null && hit.collider.CompareTag("Tiles"))
        {
            return true;
        }

        return false; 
    }
}
