using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Researcher_Work : NPC_WorkLayer<Researcher>
{
    public Researcher_Work(Layer parent, Researcher origin) : base(parent, origin)
    {
        AddState("Working", new Researcher_Working(this, origin));
    }
}
