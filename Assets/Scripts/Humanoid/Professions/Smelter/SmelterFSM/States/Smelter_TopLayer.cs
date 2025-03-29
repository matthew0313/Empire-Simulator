using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smelter_TopLayer : NPC_Layer<Smelter>
{
    public Smelter_TopLayer(Smelter origin) : base(null, origin)
    {
        defaultState = new NPC_Rest(this, origin);
        AddState("Rest", defaultState);
        AddState("Work", new Smelter_Work(this, origin));
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
