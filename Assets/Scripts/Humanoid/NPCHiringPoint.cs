using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHiringPoint : MonoBehaviour
{
    [SerializeField] GameObject m_hiringNPC;
    [SerializeField] int m_NPCLevel;
    [SerializeField] int m_cost;
    public GameObject hiringNPC { get { return m_hiringNPC; } } 
    public int NPCLevel { get { return m_NPCLevel; } }
    public int cost { get { return m_cost; } }
    readonly KeyAction jobMenuOpen = new KeyAction(KeyCode.E, 20);
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InputManager.AddAction(jobMenuOpen);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InputManager.RemoveAction(jobMenuOpen);
        }
    }
    private void Update()
    {
        if (jobMenuOpen.GetKeyDown())
        {
            UIManager.Instance.HireTab(this);
        }
    }
}
