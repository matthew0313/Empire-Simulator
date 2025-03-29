using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Movespeed Increase", menuName = "Scriptables/Buff Data/Movespeed Increase", order = 0)]
public class MovespeedIncrease : BuffData
{
    [SerializeField] float addedSpeed;
    public override void OnBuffStart(NPC buffed)
    {
        buffed.agent.speed += addedSpeed;
        base.OnBuffStart(buffed);
    }
    public override void OnBuffEnd(NPC buffed)
    {
        buffed.agent.speed -= addedSpeed;
        base.OnBuffEnd(buffed);
    }
    public override string DescribePower()
    {
        return base.DescribePower() + "+" + addedSpeed;
    }
}

