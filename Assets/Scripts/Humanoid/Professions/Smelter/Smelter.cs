using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smelter : NPC
{
    public Smelter() : base(250)
    {
        topLayer = new Smelter_TopLayer(this);
    }
    public override string Name => "Smelter";
    public override List<IWorkplace> availableJobs()
    {
        List<IWorkplace> tmp = new List<IWorkplace>();
        List<Furnace> tmp2 = island.GetAccessibles<Furnace>();
        foreach (Furnace c in tmp2)
        {
            if (c != null && c.levelRequirement <= level && c.workers.Count < c.maxWorkers) tmp.Add(c);
        }
        return tmp;
    }
    public float workAmount
    {
        get { return (1.0f + (level / 25) * 0.2f); }
    }
    public override float Luck()
    {
        return base.Luck();
    }
}
