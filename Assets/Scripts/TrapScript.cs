using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScript : MonoBehaviour
{
    [SerializeField] public GameObject bait;
    [SerializeField] public GameObject reel;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trap detected");
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Trap activated");
            reel.SetActive(true);
            bait.SetActive(false);
        }
    }
}