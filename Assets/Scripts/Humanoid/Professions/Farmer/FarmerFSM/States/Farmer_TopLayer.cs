using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmer_TopLayer : NPC_Layer<Farmer>
{
    public Farmer_TopLayer(Farmer origin) : base(null, origin)
    {
        defaultState = new NPC_Rest(this, origin);
        AddState("Rest", defaultState);
        AddState("Work", new Farmer_Work(this, origin));
        AddState("Homeless", new NPC_Homeless(this, origin));
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
