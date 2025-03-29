using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smelter_Work : NPC_WorkLayer<Smelter>
{
    public Smelter_Work(Layer parent, Smelter origin) : base(parent, origin)
    {
        AddState("Working", new Smelter_Working(this, origin));
    }
}
