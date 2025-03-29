using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NPC_Layer<T> : Layer where T : NPC
{
    protected T character;
    public NPC_Layer(Layer parent, T origin) : base(parent)
    {
        character = origin;
    }
}
public class NPC_Layer : NPC_Layer<NPC>
{
    public NPC_Layer(Layer parent, NPC origin) : base(parent, origin)
    {

    }
}

