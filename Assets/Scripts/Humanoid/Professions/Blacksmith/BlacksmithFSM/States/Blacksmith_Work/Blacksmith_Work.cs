using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blacksmith_Work : NPC_WorkLayer<Blacksmith>
{
    public Blacksmith_Work(Layer parent, Blacksmith origin) : base(parent, origin)
    {
        AddState("Working", new Blacksmith_Working(this, origin));
    }
}
