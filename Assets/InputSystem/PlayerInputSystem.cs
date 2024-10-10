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
    [SerializeField] private float speed = 12f;
    [SerializeField] private float jumpForce = 7f;

    private void Awake()
    {
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

        float horizontalMovement = inputVector.x * speed;

        body.velocity = new Vector2(horizontalMovement, body.velocity.y);

        isGrounded = CheckGround();

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
            body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            canJump = false;
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
