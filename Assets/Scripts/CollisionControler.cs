using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CollisionControler : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Water") || (collision.gameObject.tag == "Spike"))
        {
            Debug.Log("Game Over");
            SceneManager.LoadScene("DieScene", LoadSceneMode.Single);

        }

        if (collision.gameObject.tag == "Chest")
        {
            Debug.Log("You Win");
            SceneManager.LoadScene("WinScene", LoadSceneMode.Single);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "coin")
        {
            Debug.Log("Coin Collected");
            Destroy(collision.gameObject);
        }
    }
}
