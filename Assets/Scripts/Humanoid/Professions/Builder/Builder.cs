using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : NPC
{
    [SerializeField] GameObject builderHammer;
    public Builder() : base(500)
    {
        topLayer = new Builder_TopLayer(this);
    }
    public override string Name => "Builder";
    public float workAmount
    {
        get { return 1.0f + (level / 25) * 1.0f; }
    }
    protected override void Awake()
    {
        workGear = new Equipment(builderHammer);
        base.Awake();
    }
    public override List<IWorkplace> availableJobs()
    {
        List<IWorkplace> tmp = new List<IWorkplace>();
        List<Blueprint> tmp2 = island.GetAccessibles<Blueprint>();
        foreach (Blueprint c in tmp2)
        {
            if(c!=null) tmp.Add(c);
        }
        return tmp;
    }
}
