using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island : MonoBehaviour
{
    public Transform spawnPoint;
    [SerializeField] float m_distance = 0.0f;
    public float distance { get { return m_distance; } }
    [TextArea][SerializeField] string description;
    public virtual string Describe()
    {
        return description;
    }
}
