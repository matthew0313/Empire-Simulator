using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Miner : NPC
{
    public Miner() : base(250)
    {
        topLayer = new Miner_TopLayer(this);
    }
    public override string Name => "Miner";
    public float pickDamage { 
        get
        {
            return (workGear.data as PickaxeData).damage * (1.0f + level * (2.0f / 100.0f));
        } 
    }

    public OreNode targetNode;
    public Miner_OrePriority priority = Miner_OrePriority.random;
    public OreNodeData targetOreType;
    public override List<IWorkplace> availableJobs()
    {
        List<IWorkplace> tmp = new List<IWorkplace>();
        List<Cave> tmp2 = island.GetAccessibles<Cave>();
        foreach(Cave c in tmp2)
        {
            if(c!=null) tmp.Add(c);
        }
        return tmp;
    }

    public override float Luck()
    {
        return base.Luck();
    }
    public void ChangePriority(Miner_OrePriority priority)
    {
        this.priority = priority;
    }
}
