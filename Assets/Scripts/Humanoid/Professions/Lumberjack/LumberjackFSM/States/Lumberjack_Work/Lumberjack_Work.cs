using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Lumberjack_Work : NPC_WorkLayer<Lumberjack>
{
    public Lumberjack_Work(Layer parent, Lumberjack origin) : base(parent, origin)
    {
        defaultState = new NPC_FindGear<AxeData>(this, origin);
        AddState("FindGear", defaultState);
        AddState("Working", new Lumberjack_Work_Working(this, origin));
    }
    public override void OnUpdate()
    {
        if(character.workGear == null)
        {
            SwitchState("FindGear");
        }
        base.OnUpdate();
    }
}
