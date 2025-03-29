using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blacksmith : NPC
{
    [SerializeField] GameObject blacksmithHammer;
    public Blacksmith() : base(250)
    {
        topLayer = new Blacksmith_TopLayer(this);
    }
    public override string Name => "Blacksmith";
    protected override void Awake()
    {
        Equip(new Equipment(blacksmithHammer));
        base.Awake();
    }
    public override List<IWorkplace> availableJobs()
    {
        List<IWorkplace> tmp = new List<IWorkplace>();
        List<Smithery> tmp2 = island.GetAccessibles<Smithery>();
        foreach (Smithery c in tmp2)
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
