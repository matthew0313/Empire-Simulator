using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnData : ScriptableObject
{
    [SerializeField] string m_Name;
    [SerializeField] float m_maxHealth;
    [SerializeField] float m_respawnTime;

    [SerializeField] DropTable m_drops;
    [SerializeField] int m_experienceGiven;

    public string Name { get { return m_Name; } }
    public float maxHealth { get { return m_maxHealth; } }
    public float respawnTime { get {  return m_respawnTime; } }
    public DropTable drops { get { return m_drops; } }
    public int experienceGiven { get {  return m_experienceGiven; } }
}
