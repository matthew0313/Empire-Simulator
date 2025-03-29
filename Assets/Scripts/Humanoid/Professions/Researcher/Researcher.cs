using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Researcher : NPC
{
    public Researcher() : base(250)
    {
        topLayer = new Researcher_TopLayer(this);
    }
    public override string Name => "Researcher";
    public override List<IWorkplace> availableJobs()
    {
        List<IWorkplace> tmp = new List<IWorkplace>();
        List<ResearchStation> tmp2 = island.GetAccessibles<ResearchStation>();
        foreach (ResearchStation c in tmp2)
        {
            if (c != null && c.levelRequirement <= level && c.workers.Count < c.maxWorkers) tmp.Add(c);
        }
        return tmp;
    }
    public float workAmount
    {
        get { return (1.0f + (level / 25) * 0.5f); }
    }
    public override float Luck()
    {
        return base.Luck();
    }
}
