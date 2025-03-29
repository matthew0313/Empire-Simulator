using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lumberjack : NPC
{
    public Lumberjack() : base(250)
    {
        topLayer = new Lumberjack_TopLayer(this);
    }
    public override string Name => "Lumberjack";
    public float pickDamage
    {
        get
        {
            return (workGear.data as AxeData).damage * (1.0f + level * (2.0f / 100.0f));
        }
    }
    public TreeSpawn targetTree;
    public Lumberjack_TreePriority priority = Lumberjack_TreePriority.random;
    public override List<IWorkplace> availableJobs()
    {
        List<IWorkplace> tmp = new List<IWorkplace>();
        List<Forest> tmp2 = island.GetAccessibles<Forest>();
        foreach (Forest c in tmp2)
        {
            if (c != null) tmp.Add(c);
        }
        return tmp;
    }
    public override float Luck()
    {
        return base.Luck();
    }
}
