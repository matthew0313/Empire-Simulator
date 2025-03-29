using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Sickle Data", menuName = "Scriptables/Item Datas/Equipments/Sickle Data", order = 0)]
public class SickleData : EquipmentData
{
    [SerializeField] float m_workMultiplier;

    public float workMultiplier { get { return m_workMultiplier; } }
}
public class Sickle
{
    public Sickle(SickleData dt)
    {
        data = dt;
        durability = dt.durability;
    }
    public SickleData data;
    public int durability;
}
