using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmer : NPC
{
    public Farmer() : base(250)
    {
        topLayer = new Farmer_TopLayer(this);
    }
    public override string Name => "Farmer";

    public float workAmount
    {
        get
        {
            return (1.0f + (level / 25) * 0.5f) * (workGear.data as SickleData).workMultiplier;
        }
    }
    public override List<IWorkplace> availableJobs()
    {
        List<IWorkplace> tmp = new List<IWorkplace>();
        List<Farm> tmp2 = island.GetAccessibles<Farm>();
        foreach (Farm c in tmp2)
        {
            if(c.maxWorkers > c.workers.Count && c!=null) tmp.Add(c);
        }
        return tmp;
    }
}
