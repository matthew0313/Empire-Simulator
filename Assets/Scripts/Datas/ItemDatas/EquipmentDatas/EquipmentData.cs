using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EquipmentData : ItemData
{
    [SerializeField] int m_durability;
    [SerializeField] GameObject m_model;
    public int durability { get {  return m_durability; } }
    public GameObject model { get { return m_model; } }
    public int priority;

    public override string Describe()
    {
        return base.Describe() + "\n\n" + "Durability: " + durability;
    }
}
public class Equipment
{
    public Equipment(EquipmentData dt)
    {
        data = dt;
        durability = dt.durability;
        model = MonoBehaviour.Instantiate(dt.model);
    }
    public Equipment(GameObject model)
    {
        this.model = model;
    }
    public GameObject model;
    public EquipmentData data;
    public int durability;
}
