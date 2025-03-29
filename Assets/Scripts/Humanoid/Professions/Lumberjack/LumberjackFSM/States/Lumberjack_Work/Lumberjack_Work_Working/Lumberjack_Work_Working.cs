using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lumberjack_Work_Working : NPC_Layer<Lumberjack>
{
    public Lumberjack_Work_Working(Layer parent, Lumberjack origin) : base(parent, origin)
    {
        defaultState = new Lumberjack_Work_Working_LookForTrees(this, origin);
        AddState("LookForTrees", defaultState);
        AddState("MoveToTree", new Lumberjack_Work_Working_MoveToTree(this, origin));  
        AddState("Chopping", new Lumberjack_Work_Working_Chopping(this, origin));
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}
