using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OreNode Data", menuName = "Scriptables/Spawn Data/OreNode Data", order = 0)]
public class OreNodeData : SpawnData
{
    public static List<OreNodeData> oreNodeDatas = new List<OreNodeData>();
    public OreNodeData()
    {
        oreNodeDatas.Add(this);
    }
    [SerializeField] int m_tier;
    public int tier { get {  return m_tier; } }
}
