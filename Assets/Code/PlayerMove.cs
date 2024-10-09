using UnityEngine;
using UnityEngine.Events;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D body;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>(); 
    }

    private void Update()
    {
        var supplement = 4;
        var move = new Vector2(Input.GetAxis("Horizontal")*supplement, body.velocity.y);
        if (Input.GetButtonDown("Jump"))
        {
            move.y = 10;
        }
        
        body.velocity = move;
    }
}
