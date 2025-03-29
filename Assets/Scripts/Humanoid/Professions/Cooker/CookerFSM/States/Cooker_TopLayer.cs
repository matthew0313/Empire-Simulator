using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooker_TopLayer : NPC_Layer<Cooker>
{
    public Cooker_TopLayer(Cooker origin) : base(null, origin)
    {
        defaultState = new NPC_Rest(this, origin);
        AddState("Rest", defaultState);
        AddState("Work", new Cooker_Work(this, origin));
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
