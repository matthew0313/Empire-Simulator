using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreNode : MonoBehaviour
{
    [SerializeField] OreNodeData data;
    public OreNodeData oreType { get { return data; } }
    public string Name { get { return data.Name; } }
    public int tier { get { return data.tier; } }
    float health;

    public NPC occupant = null;

    public bool oreSpawned = true;
    float counter = 0.0f;
    [SerializeField] GameObject ore;

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
            oreSpawned = false;
            occupant = null;
            ore.SetActive(false);
        }
    }
    private void Update()
    {
        if (!oreSpawned)
        {
            if (counter < data.respawnTime) counter += Time.deltaTime;
            else
            {
                counter = 0.0f;
                oreSpawned = true;
                health = data.maxHealth;
                ore.SetActive(true);
            }
        }
    }
}
