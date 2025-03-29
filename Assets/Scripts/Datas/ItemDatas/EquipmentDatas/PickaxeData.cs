using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Pickaxe Data", menuName = "Scriptables/Item Datas/Equipments/Pickaxe Data", order = 0)]
public class PickaxeData : EquipmentData
{
    [SerializeField] float m_damage;
    [SerializeField] int m_tier;

    public float damage { get { return m_damage; } }
    public int tier { get { return m_tier; } }
    public override string Describe()
    {
        return base.Describe() + "\n" + "Damage: " + damage + "\n" + "Tier: " + tier;
    }
}
public class Pickaxe
{
    public Pickaxe(PickaxeData dt)
    {
        data = dt;
        durability = dt.durability;
    }
    public PickaxeData data;
    public int durability;
}
