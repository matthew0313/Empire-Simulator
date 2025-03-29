using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TravelDistResearch : ResearchNode
{
    [SerializeField] int newTravelDist;
    public override string DescribeUnlock()
    {
        return $"Travel Distance: {newTravelDist}";
    }
    public override void OnResearchComplete()
    {
        EmpireManager.Instance.TravelDistEdit(newTravelDist);
        base.OnResearchComplete();
    }
}
