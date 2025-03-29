using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ResearchNode : MonoBehaviour
{
    [SerializeField] Image[] images;
    public string Name { get { return name; } }
    [SerializeField] int m_tier;
    public int tier { get { return m_tier; } }
    public bool completed { get; private set; } = false;
    [SerializeField] bool m_available = false;
    public bool available { get { return m_available; } }
    [SerializeField] float m_requiredWorkAmount;
    public float requiredWorkAmount { get { return m_requiredWorkAmount; } }
    public float workedAmount { get; private set; }
    public List<ItemIntPair> ingredients = new();
    public bool paid = false;
    [SerializeField] List<ResearchNode> unlocks = new();

    static Color32 lockedColor = new Color32(70, 70, 70, 255);
    static Color32 unlockedColor = new Color32(140, 140, 140, 255);
    static Color32 completedColor = new Color32(255, 255, 255, 255);
    private void Awake()
    {
        ChangeColor();
    }
    void ChangeColor()
    {
        foreach (var i in images)
        {
            i.color = completed ? completedColor : available ? unlockedColor : lockedColor;
        }
    }
    public virtual void OnResearchComplete()
    {
        completed = true;
        ChangeColor();
        foreach (var i in unlocks) i.Unlock();
    }
    public void Progress(float workAmount)
    {
        workedAmount = Mathf.Min(requiredWorkAmount, workedAmount + workAmount);
    }
    public void Unlock()
    {
        if (available) return;
        m_available = true;
        ChangeColor();
    }
    public virtual string DescribeUnlock()
    {
        return "Unlocks:";
    }
}
