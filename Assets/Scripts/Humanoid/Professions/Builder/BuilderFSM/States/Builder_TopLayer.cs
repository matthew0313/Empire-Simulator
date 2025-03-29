using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder_TopLayer : NPC_Layer<Builder>
{
    public Builder_TopLayer(Builder origin) : base(null, origin)
    {
        defaultState = new NPC_Rest(this, origin);
        AddState("Rest", defaultState);
        AddState("Work", new Builder_Work(this, origin));
        AddState("Homeless", new NPC_Homeless(this, origin));
    }
    public override void OnUpdate()
    {
        if (character.home == null && CurrentState() != "Homeless")
        {
            SwitchState("Homeless");
        }
        base.OnUpdate();
    }
}
