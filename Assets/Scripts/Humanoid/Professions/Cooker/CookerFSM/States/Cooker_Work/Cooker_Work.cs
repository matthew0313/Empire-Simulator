using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooker_Work : NPC_WorkLayer<Cooker>
{
    public Cooker_Work(Layer parent, Cooker origin) : base(parent, origin)
    {
        AddState("Working", new Cooker_Working(this, origin));
    }
}
