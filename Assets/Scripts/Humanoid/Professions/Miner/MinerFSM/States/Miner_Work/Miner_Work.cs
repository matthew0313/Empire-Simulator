using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Miner_Work : NPC_WorkLayer<Miner>
{
    public Miner_Work(Layer parent, Miner origin) : base(parent, origin)
    {
        defaultState = new NPC_FindGear<PickaxeData>(this, origin);
        AddState("FindGear", defaultState);
        AddState("Working", new Miner_Work_Working(this, origin));
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
