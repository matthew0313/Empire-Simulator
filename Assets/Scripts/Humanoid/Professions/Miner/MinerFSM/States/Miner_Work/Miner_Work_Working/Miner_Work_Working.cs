using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner_Work_Working : NPC_Layer<Miner>
{
    public Miner_Work_Working(Layer parent, Miner origin) : base(parent, origin)
    {
        defaultState = new Miner_Work_Working_LookForOres(this, origin);
        AddState("LookForOres", defaultState);
        AddState("MoveToOre", new Miner_Work_Working_MoveToOre(this, origin));  
        AddState("Mining", new Miner_Work_Working_Mining(this, origin));
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}
