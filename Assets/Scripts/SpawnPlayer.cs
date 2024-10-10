using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject player;

    void Start()
    {
        Vector3 spawnPoint = GameObject.Find("SpawnPoint").transform.position;
        player = Instantiate(playerPrefab, spawnPoint, Quaternion.identity);
        player.AddComponent<PlayerInputSystem>();
        
        GameObject.Find("Cinemachine").GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = player.transform;
    }
}
