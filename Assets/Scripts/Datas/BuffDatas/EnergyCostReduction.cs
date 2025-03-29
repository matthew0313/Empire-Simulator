using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Energy Cost Reduction", menuName = "Scriptables/Buff Data/Energy Cost Reduction", order = 0)]
public class EnergyCostReduction : BuffData
{
    [SerializeField] float energyCostMultiplier = 1.0f;
    public float ModifyEnergyCost(float value)
    {
        return value * energyCostMultiplier;
    }
    public override string DescribePower()
    {
        return base.DescribePower() + "x" + energyCostMultiplier;
    }
}

