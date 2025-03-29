using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NPC_State<T> : State
{
    protected T character;
    public NPC_State(Layer parent, T origin) : base(parent)
    {
        character = origin;
    }
}
public class NPC_State : NPC_State<NPC>
{
    public NPC_State(Layer parent, NPC origin) : base(parent, origin)
    {

    }
}

