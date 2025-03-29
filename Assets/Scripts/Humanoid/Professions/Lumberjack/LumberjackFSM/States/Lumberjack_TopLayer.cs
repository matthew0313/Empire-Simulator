using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lumberjack_TopLayer : NPC_Layer<Lumberjack>
{
    public Lumberjack_TopLayer(Lumberjack origin) : base(null, origin)
    {
        defaultState = new NPC_Rest(this, character);
        AddState("Rest", defaultState);
        AddState("Work", new Lumberjack_Work(this, character));
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
