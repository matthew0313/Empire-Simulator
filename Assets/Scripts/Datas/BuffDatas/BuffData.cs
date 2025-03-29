using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BuffData : ScriptableObject
{
    [TextArea][SerializeField] string m_Description;
    public string Name { get { return name; } }
    public string Description { get { return m_Description; } }
    public virtual string DescribePower()
    {
        return null;
    }
    public virtual void OnBuffStart(NPC buffed)
    {

    }
    public virtual void OnBuffEnd(NPC buffed)
    {

    }
}
[System.Serializable] public class Buff
{
    public static List<Buff> pool = new List<Buff>();
    public void Set(BuffData dt, float duration, NPC own)
    {
        buff = dt;
        timeLeft = duration;
        owner = own;
        buff.OnBuffStart(own);
    }

    NPC owner;
    public BuffData buff;
    public float timeLeft;
    public bool Active
    {
        get { return timeLeft <= 0; }
    }
    public void OnUpdate()
    {
        if(timeLeft <= 0)
        {
            owner.buffs.Remove(this);
            buff.OnBuffEnd(owner);
            pool.Add(this);
        }
        else timeLeft -= Time.deltaTime;
    }
}
