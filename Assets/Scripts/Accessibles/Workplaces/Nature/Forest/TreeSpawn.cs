using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawn : MonoBehaviour
{
    [SerializeField] TreeSpawnData data;
    public string Name { get { return data.Name; } }
    float health;

    public NPC occupant = null;

    public bool treeSpawned = true;
    float counter = 0.0f;
    [SerializeField] GameObject tree;

    private void Start()
    {
        health = data.maxHealth;
    }
    public void GetDamage(Humanoid damager, float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            EmpireManager.Instance.AddItemToStorage(data.drops.GetDrop(damager.Luck()));
            if (typeof(NPC).IsAssignableFrom(damager.GetType())) (damager as NPC).GetExperience(data.experienceGiven);
            treeSpawned = false;
            occupant = null;
            tree.SetActive(false);
        }
    }
    private void Update()
    {
        if (!treeSpawned)
        {
            if (counter < data.respawnTime) counter += Time.deltaTime;
            else
            {
                counter = 0.0f;
                treeSpawned = true;
                health = data.maxHealth;
                tree.SetActive(true);
            }
        }
    }
}
