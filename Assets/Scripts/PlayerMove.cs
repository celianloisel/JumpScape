using UnityEngine;
using UnityEngine.Events;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D body;
    private bool isGrounded;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>(); 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Tiles")
        {
            isGrounded = true;
        }
    }

    private void Update()
    {
        var supplement = 4;
        var move = new Vector2(Input.GetAxis("Horizontal")*supplement, body.velocity.y);
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            move.y = 10;
            isGrounded = false;
        }
        
        body.velocity = move;
    }

}
