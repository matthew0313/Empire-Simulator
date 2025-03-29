using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Cooker : NPC
{
    public Cooker() : base(250)
    {
        topLayer = new Cooker_TopLayer(this);
    }
    public override string Name => "Cooker";
    public override List<IWorkplace> availableJobs()
    {
        List<IWorkplace> tmp = new List<IWorkplace>();
        List<CookStation> tmp2 = island.GetAccessibles<CookStation>();
        foreach (CookStation c in tmp2)
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
