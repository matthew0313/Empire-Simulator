using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blacksmith_TopLayer : NPC_Layer<Blacksmith>
{
    public Blacksmith_TopLayer(Blacksmith origin) : base(null, origin)
    {
        defaultState = new NPC_Rest(this, origin);
        AddState("Rest", defaultState);
        AddState("Work", new Blacksmith_Work(this, origin));
        AddState("Homeless", new NPC_Homeless(this, origin));
    }
}
