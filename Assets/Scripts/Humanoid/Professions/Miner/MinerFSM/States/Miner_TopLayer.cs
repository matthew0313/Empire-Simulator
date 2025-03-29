using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner_TopLayer : NPC_Layer<Miner>
{
    public Miner_TopLayer(Miner origin) : base(null, origin)
    {
        defaultState = new NPC_Rest(this, character);
        AddState("Rest", defaultState);
        AddState("Work", new Miner_Work(this, character));
        AddState("Homeless", new NPC_Homeless(this, character));
    }
    public override void OnUpdate()
    {
        if (character.home == null)
        {
            SwitchState("Homeless");
        }
        base.OnUpdate();
    }
}
