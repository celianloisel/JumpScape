using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputSystem : MonoBehaviour
{
    private Rigidbody2D body;
    private PlayerInput playerInput;
    private PlayerInputActions playerInputActions;
    private Animator anim;
    private bool isGrounded;
    private bool isFlip;
    private bool canJump;

    [SerializeField] private float groundCheckDistance = 0.1f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float speed = 12f;
    [SerializeField] private float jumpForce = 7f;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        anim = GetComponent<Animator>();

        anim.SetBool("isPlayerRunning", false);
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
        if(body.velocity.x != 0)
        {
            if (body.velocity.x > 0 && isFlip)
            {
                Flip();
            }
            else if (body.velocity.x < 0 && !isFlip)
            {
                Flip();
            }
            anim.SetBool("isPlayerRunning", true);
        }
        else
        {
            anim.SetBool("isPlayerRunning", false);
        }
        if(body.velocity.y > 0.5)
        {
            anim.SetBool("isJumping", true);
        }
        if (!isGrounded && body.velocity.y < -0.5f)
        {
            anim.SetBool("isFalling", true);
        }
        else if(body.velocity.y >= -0.5 && body.velocity.y <= 0.5)
        {
            anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", false);
        }
        Vector2 rayOrigin = new Vector2(transform.position.x, transform.position.y - 0.5f);
        Debug.DrawRay(rayOrigin, Vector2.down * groundCheckDistance, Color.red);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded && canJump)
        {
            anim.SetBool("isJumping", true);
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

    private void Flip()
    {
        isFlip = !isFlip;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
