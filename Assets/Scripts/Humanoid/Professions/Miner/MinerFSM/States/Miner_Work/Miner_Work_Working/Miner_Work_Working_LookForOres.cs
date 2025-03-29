using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner_Work_Working_LookForOres : NPC_State<Miner>
{
    public Miner_Work_Working_LookForOres(Layer parent, Miner origin) : base(parent, origin)
    {
        
    }
    public override void OnUpdate()
    {
        if (character.targetNode == null)
        {
            List<OreNode> available = new List<OreNode>();
            foreach (OreNode node in (character.workplace as Cave).nodes)
            {
                if (node.oreSpawned && node.occupant == null && (character.workGear.data as PickaxeData).tier >= node.tier)
                {
                    available.Add(node);
                }
            }
            if(available.Count > 0)
            {
                if(character.priority == Miner_OrePriority.random)
                {
                    OreSelect(available[Random.Range(0, available.Count)]);
                }
                else if (character.priority == Miner_OrePriority.highTier)
                {
                    available.Sort((OreNode a, OreNode b) => { return b.tier.CompareTo(a.tier); });
                    OreSelect(available[0]);
                }
                else if(character.priority == Miner_OrePriority.lowTier)
                {
                    available.Sort((OreNode a, OreNode b) => { return a.tier.CompareTo(b.tier); });
                    OreSelect(available[0]);
                }
                else if(character.priority == Miner_OrePriority.preferSetOre)
                {
                    if (character.targetOreType != null)
                    {
                        bool chose = false;
                        foreach(var i in available)
                        {
                            if(i.oreType == character.targetOreType)
                            {
                                OreSelect(i);
                                chose = true;
                                break;
                            }
                        }
                        if (!chose)
                        {
                            OreSelect(available[Random.Range(0, available.Count)]);
                        }
                    }
                    else
                    {
                        OreSelect(available[Random.Range(0, available.Count)]);
                    }
                }
                parentLayer.SwitchState("MoveToOre");
            }
        }
        else parentLayer.SwitchState("MoveToOre");
        base.OnUpdate();
    }
    void OreSelect(OreNode target)
    {
        character.targetNode = target;
        target.occupant = character;
    }
}
[System.Serializable] public enum Miner_OrePriority
{
    random,
    highTier,
    lowTier,
    preferSetOre
}
