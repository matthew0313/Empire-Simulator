using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NPC_Homeless : NPC_State
{
    public NPC_Homeless(Layer parent, NPC origin) : base(parent, origin)
    {

    }
    public override void OnUpdate()
    {
        if(character.home != null)
        {
            parentLayer.SwitchState("Rest");
        }
        base.OnUpdate();
    }
}
