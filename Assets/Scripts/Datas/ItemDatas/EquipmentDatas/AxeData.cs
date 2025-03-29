using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Axe Data", menuName = "Scriptables/Item Datas/Equipments/Axe Data", order = 0)]
public class AxeData : EquipmentData
{
    [SerializeField] float m_damage;
    public float damage { get { return m_damage; } }
    public override string Describe()
    {
        return base.Describe() + "\n" + "Damage: " + damage;
    }
}
public class Axe
{
    public Axe(AxeData dt)
    {
        data = dt;
        durability = dt.durability;
    }
    public AxeData data;
    public int durability;
}
