using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder_Work : NPC_WorkLayer<Builder>
{
    public Builder_Work(Layer parent, Builder origin) : base(parent, origin)
    {
        AddState("Working", new Builder_Work_Working(this, origin));
    }
}
