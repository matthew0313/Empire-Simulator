using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmer_Work : NPC_WorkLayer<Farmer>
{
    public Farmer_Work(Layer parent, Farmer origin) : base(parent, origin)
    {
        defaultState = new NPC_FindGear<SickleData>(this, origin);
        AddState("FindGear", defaultState);
        AddState("Working", new Farmer_Work_Working(this, origin));
    }
    public override void OnUpdate()
    {
        if (character.workGear == null)
        {
            SwitchState("FindGear");
        }
        base.OnUpdate();
    }
}
