using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ocean : MonoBehaviour
{
    Transform player;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void LateUpdate()
    {
        if (player.position.y-1.0f < transform.position.y)
        {
            UIManager.Instance.Log("Teleported to spawn", "You fell into the ocean and was teleported back to the spawn point of the island. If you want to travel to other islands, press M to open travel menu.", new Color(0, 1, 0.5f));
            player.position = EmpireManager.Instance.currentIsland.spawnPoint.position;
        }
    }
}
