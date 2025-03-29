using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

[CreateAssetMenu(fileName = "Food Data", menuName = "Scriptables/Item Datas/Food Data/Food", order = 0)]
public class FoodData : ItemData
{
    [SerializeField] float m_energy;
    public float energy { get { return m_energy; } }
    public int priority;

    public virtual void OnEat(NPC eater)
    {
        eater.energy += energy;
    }
    public override string Describe()
    {
        return base.Describe() + "\n\n" + "Energy: " + energy;
    }
}
